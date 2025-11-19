using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using fiexpress.Data;
using fiexpress.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.Data.SqlClient;
using System.Text;

namespace fiexpress.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EmpleadosController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly ILogger<EmpleadosController> _logger;
        private readonly IConfiguration _configuration;

        public EmpleadosController(MyDbContext context, ILogger<EmpleadosController> logger, IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        // GET: api/empleados
        [HttpGet]
        public async Task<IActionResult> GetEmpleados([FromQuery] bool incluirInactivos = false)
        {
            try
            {
                var query = _context.Empleados
                    .Include(e => e.Departamento)
                    .Include(e => e.Rol)
                    .AsQueryable();

                if (!incluirInactivos)
                {
                    query = query.Where(e => e.activo);
                }

                var empleados = await query
                    .Select(e => new
                    {
                        e.idEmpleado,
                        e.codigo_empleado,
                        e.nombre,
                        e.email,
                        e.telefono,
                        e.activo,
                        fecha_ingreso = e.fecha_ingreso.ToString("yyyy-MM-dd"),
                        fecha_baja = e.fecha_baja.HasValue ? e.fecha_baja.Value.ToString("yyyy-MM-dd") : null,
                        fecha_nacimiento = e.fecha_nacimiento.ToString("yyyy-MM-dd"),
                        e.foto_url,
                        departamento = e.Departamento.nombre,
                        rol = e.Rol.nombre,
                        tieneUsuario = e.Usuario != null,
                        esSupervisor = e.Supervisor != null
                    })
                    .OrderBy(e => e.nombre)
                    .ToListAsync();

                return Ok(empleados);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener empleados");
                return StatusCode(500, new { mensaje = "Error al obtener empleados", error = ex.Message });
            }
        }

        // GET: api/empleados/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEmpleado(int id)
        {
            try
            {
                var empleado = await _context.Empleados
                    .Include(e => e.Departamento)
                    .Include(e => e.Rol)
                    .Where(e => e.idEmpleado == id)
                    .Select(e => new
                    {
                        e.idEmpleado,
                        e.codigo_empleado,
                        e.nombre,
                        e.email,
                        e.telefono,
                        fecha_nacimiento = e.fecha_nacimiento.ToString("yyyy-MM-dd"),
                        fecha_ingreso = e.fecha_ingreso.ToString("yyyy-MM-dd"),
                        fecha_baja = e.fecha_baja.HasValue ? e.fecha_baja.Value.ToString("yyyy-MM-dd") : null,
                        e.foto_url,
                        e.activo,
                        departamento = new { e.Departamento.idDepartamento, e.Departamento.nombre },
                        rol = new { e.Rol.idRol, e.Rol.nombre }
                    })
                    .FirstOrDefaultAsync();

                if (empleado == null)
                {
                    return NotFound(new { mensaje = "Empleado no encontrado" });
                }

                return Ok(empleado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener empleado {Id}", id);
                return StatusCode(500, new { mensaje = "Error al obtener empleado" });
            }
        }

        // POST: api/empleados - VERSIÓN CON AVATAR AUTOMÁTICO
        [HttpPost]
        [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> CrearEmpleado([FromBody] EmpleadoCreateDto dto)
        {
            SqlConnection connection = null;
            try
            {
                _logger.LogInformation(" DTO recibido: {@Dto}", dto);

                // Validaciones básicas
                if (string.IsNullOrWhiteSpace(dto.CodigoEmpleado))
                    return BadRequest(new { mensaje = "El código de empleado es obligatorio" });

                if (string.IsNullOrWhiteSpace(dto.Nombre))
                    return BadRequest(new { mensaje = "El nombre es obligatorio" });

                if (string.IsNullOrWhiteSpace(dto.Email))
                    return BadRequest(new { mensaje = "El email es obligatorio" });

                // Validar código único
                if (await _context.Empleados.AnyAsync(e => e.codigo_empleado == dto.CodigoEmpleado))
                {
                    return BadRequest(new { mensaje = "El código de empleado ya existe" });
                }

                // Validar email único
                if (await _context.Empleados.AnyAsync(e => e.email == dto.Email))
                {
                    return BadRequest(new { mensaje = "El email ya existe" });
                }

                // Validar departamento
                var departamento = await _context.Departamentos
                    .FirstOrDefaultAsync(d => d.idDepartamento == dto.IdDepartamento && d.activo);

                if (departamento == null)
                {
                    return BadRequest(new { mensaje = "El departamento no existe o está inactivo" });
                }

                // Validar rol
                var rol = await _context.Roles
                    .FirstOrDefaultAsync(r => r.idRol == dto.IdRol && r.activo);

                if (rol == null)
                {
                    return BadRequest(new { mensaje = "El rol no existe o está inactivo" });
                }

                // ✅ CORRECCIÓN: Conversión segura de fechas
                if (!DateOnly.TryParse(dto.FechaNacimiento, out DateOnly fechaNacimiento))
                {
                    return BadRequest(new { mensaje = "Formato de fecha de nacimiento inválido. Use YYYY-MM-DD" });
                }

                if (!DateOnly.TryParse(dto.FechaIngreso, out DateOnly fechaIngreso))
                {
                    return BadRequest(new { mensaje = "Formato de fecha de ingreso inválido. Use YYYY-MM-DD" });
                }

                // Validar que la fecha de nacimiento sea razonable
                if (fechaNacimiento > DateOnly.FromDateTime(DateTime.Now.AddYears(-14)))
                {
                    return BadRequest(new { mensaje = "El empleado debe tener al menos 14 años" });
                }

                // ✅ GENERAR AVATAR AUTOMÁTICO CON INICIALES
                string fotoUrl = dto.FotoUrl?.Trim();
                if (string.IsNullOrWhiteSpace(fotoUrl))
                {
                    fotoUrl = GenerarAvatarUrl(dto.Nombre);
                    _logger.LogInformation("🎨 Avatar generado automáticamente: {AvatarUrl}", fotoUrl);
                }

                // ✅ SOLUCIÓN DEFINITIVA: SQL DIRECTO
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                connection = new SqlConnection(connectionString);
                await connection.OpenAsync();

                var sql = @"
                    INSERT INTO empleado (
                        idDepartamento, idRol, codigo_empleado, nombre, email, telefono,
                        fecha_nacimiento, fecha_ingreso, foto_url, activo
                    ) VALUES (
                        @IdDepartamento, @IdRol, @CodigoEmpleado, @Nombre, @Email, @Telefono,
                        @FechaNacimiento, @FechaIngreso, @FotoUrl, 1
                    );
                    SELECT SCOPE_IDENTITY();";

                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@IdDepartamento", dto.IdDepartamento);
                command.Parameters.AddWithValue("@IdRol", dto.IdRol);
                command.Parameters.AddWithValue("@CodigoEmpleado", dto.CodigoEmpleado.Trim());
                command.Parameters.AddWithValue("@Nombre", dto.Nombre.Trim());
                command.Parameters.AddWithValue("@Email", dto.Email.Trim().ToLower());
                command.Parameters.AddWithValue("@Telefono", dto.Telefono.Trim());
                command.Parameters.AddWithValue("@FechaNacimiento", fechaNacimiento);
                command.Parameters.AddWithValue("@FechaIngreso", fechaIngreso);
                command.Parameters.AddWithValue("@FotoUrl", fotoUrl);

                _logger.LogInformation("💾 Ejecutando INSERT con SQL directo - activo = 1");

                var nuevoId = await command.ExecuteScalarAsync();
                var idEmpleado = Convert.ToInt32(nuevoId);

                _logger.LogInformation("✅ Empleado creado exitosamente con ID: {Id}", idEmpleado);

                // Registrar auditoría
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (int.TryParse(userId, out int usuarioId))
                {
                    var auditoria = new Auditoria
                    {
                        idAuditoriaUsuario = usuarioId,
                        accion = "CREAR_EMPLEADO",
                        entidad_afectada = "Empleado",
                        fecha_accion = DateTime.Now,
                        ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown",
                        descripcion = $"Empleado creado: {dto.Nombre} ({dto.CodigoEmpleado})"
                    };
                    _context.Auditorias.Add(auditoria);
                    await _context.SaveChangesAsync();
                }

                return Ok(new
                {
                    mensaje = "Empleado creado exitosamente",
                    id = idEmpleado,
                    avatarGenerado = string.IsNullOrWhiteSpace(dto.FotoUrl) // Indicar si se generó avatar
                });
            }
            catch (SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "❌ Error SQL al crear empleado");
                return StatusCode(500, new
                {
                    mensaje = "Error de base de datos al crear empleado",
                    error = sqlEx.Message,
                    numeroError = sqlEx.Number
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Error al crear empleado");
                return StatusCode(500, new
                {
                    mensaje = "Error al crear empleado",
                    error = ex.Message
                });
            }
            finally
            {
                connection?.Close();
                connection?.Dispose();
            }
        }

        // PUT: api/empleados/{id}
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> ActualizarEmpleado(int id, [FromBody] EmpleadoUpdateDto dto)
        {
            try
            {
                var empleado = await _context.Empleados.FindAsync(id);
                if (empleado == null)
                {
                    return NotFound(new { mensaje = "Empleado no encontrado" });
                }

                // Validar email único (si cambió)
                if (dto.Email != empleado.email &&
                    await _context.Empleados.AnyAsync(e => e.email == dto.Email && e.idEmpleado != id))
                {
                    return BadRequest(new { mensaje = "El email ya existe" });
                }

                // Validar departamento
                var departamento = await _context.Departamentos
                    .FirstOrDefaultAsync(d => d.idDepartamento == dto.IdDepartamento && d.activo);

                if (departamento == null)
                {
                    return BadRequest(new { mensaje = "El departamento no existe o está inactivo" });
                }

                // Validar rol
                var rol = await _context.Roles
                    .FirstOrDefaultAsync(r => r.idRol == dto.IdRol && r.activo);

                if (rol == null)
                {
                    return BadRequest(new { mensaje = "El rol no existe o está inactivo" });
                }

                // ✅ ACTUALIZAR AVATAR SI NO TIENE FOTO
                if (string.IsNullOrWhiteSpace(empleado.foto_url) || empleado.foto_url == "/images/default-avatar.png")
                {
                    empleado.foto_url = GenerarAvatarUrl(dto.Nombre);
                    _logger.LogInformation("🎨 Avatar actualizado automáticamente: {AvatarUrl}", empleado.foto_url);
                }

                // Actualizar empleado
                empleado.nombre = dto.Nombre.Trim();
                empleado.email = dto.Email.Trim().ToLower();
                empleado.telefono = dto.Telefono.Trim();
                empleado.idDepartamento = dto.IdDepartamento;
                empleado.idRol = dto.IdRol;

                await _context.SaveChangesAsync();

                // Registrar auditoría
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (int.TryParse(userId, out int usuarioId))
                {
                    var auditoria = new Auditoria
                    {
                        idAuditoriaUsuario = usuarioId,
                        accion = "ACTUALIZAR_EMPLEADO",
                        entidad_afectada = "Empleado",
                        fecha_accion = DateTime.Now,
                        ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown",
                        descripcion = $"Empleado actualizado: {empleado.nombre} ({empleado.codigo_empleado})"
                    };
                    _context.Auditorias.Add(auditoria);
                    await _context.SaveChangesAsync();
                }

                return Ok(new { mensaje = "Empleado actualizado exitosamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar empleado {Id}", id);
                return StatusCode(500, new { mensaje = "Error al actualizar empleado" });
            }
        }

        // PUT: api/empleados/{id}/desactivar (MEJORADO)
        [HttpPut("{id:int}/desactivar")]
        [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> DesactivarEmpleado(int id)
        {
            try
            {
                var empleado = await _context.Empleados.FindAsync(id);
                if (empleado == null)
                {
                    return NotFound(new { mensaje = "Empleado no encontrado" });
                }

                if (!empleado.activo)
                {
                    return Ok(new { mensaje = "El empleado ya está inactivo" }); // ✅ Cambiado a 200 OK
                }

                empleado.activo = false;
                empleado.fecha_baja = DateOnly.FromDateTime(DateTime.Now);

                await _context.SaveChangesAsync();

                // ✅ AUDITORÍA MEJORADA - Manejo más robusto
                try
                {
                    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    if (int.TryParse(userId, out int usuarioId))
                    {
                        var auditoria = new Auditoria
                        {
                            idAuditoriaUsuario = usuarioId,
                            accion = "DESACTIVAR_EMPLEADO",
                            entidad_afectada = "Empleado",
                            fecha_accion = DateTime.Now,
                            ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown",
                            descripcion = $"Empleado desactivado: {empleado.nombre} ({empleado.codigo_empleado})"
                        };
                        _context.Auditorias.Add(auditoria);
                        await _context.SaveChangesAsync(); // ✅ Segundo SaveChanges solo para auditoría
                    }
                }
                catch (Exception auditEx)
                {
                    _logger.LogWarning(auditEx, "Advertencia: No se pudo registrar auditoría, pero el empleado fue desactivado");
                    // ✅ NO relanzamos la excepción - la operación principal fue exitosa
                }

                return Ok(new { mensaje = "Empleado desactivado exitosamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al desactivar empleado {Id}", id);
                return StatusCode(500, new { mensaje = "Error al desactivar empleado" });
            }
        }

        // PUT: api/empleados/{id}/activar (MEJORADO)
        [HttpPut("{id:int}/activar")]
        [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> ActivarEmpleado(int id)
        {
            try
            {
                var empleado = await _context.Empleados.FindAsync(id);
                if (empleado == null)
                {
                    return NotFound(new { mensaje = "Empleado no encontrado" });
                }

                if (empleado.activo)
                {
                    return Ok(new { mensaje = "El empleado ya está activo" }); // ✅ Cambiado a 200 OK
                }

                empleado.activo = true;
                empleado.fecha_baja = null;

                await _context.SaveChangesAsync();

                // ✅ AUDITORÍA MEJORADA - Manejo más robusto
                try
                {
                    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    if (int.TryParse(userId, out int usuarioId))
                    {
                        var auditoria = new Auditoria
                        {
                            idAuditoriaUsuario = usuarioId,
                            accion = "ACTIVAR_EMPLEADO",
                            entidad_afectada = "Empleado",
                            fecha_accion = DateTime.Now,
                            ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown",
                            descripcion = $"Empleado activado: {empleado.nombre} ({empleado.codigo_empleado})"
                        };
                        _context.Auditorias.Add(auditoria);
                        await _context.SaveChangesAsync(); // ✅ Segundo SaveChanges solo para auditoría
                    }
                }
                catch (Exception auditEx)
                {
                    _logger.LogWarning(auditEx, "Advertencia: No se pudo registrar auditoría, pero el empleado fue activado");
                    // ✅ NO relanzamos la excepción - la operación principal fue exitosa
                }

                return Ok(new { mensaje = "Empleado activado exitosamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al activar empleado {Id}", id);
                return StatusCode(500, new { mensaje = "Error al activar empleado" });
            }
        }

        // ✅ MÉTODO PARA GENERAR AVATAR CON INICIALES
        private string GenerarAvatarUrl(string nombre)
        {
            try
            {
                // Extraer iniciales del nombre
                var iniciales = ObtenerIniciales(nombre);

                // Colores base para el avatar (puedes personalizar esta paleta)
                var colores = new[] { "0d6efd", "198754", "dc3545", "fd7e14", "6f42c1", "0dcaf0", "20c997", "ffc107" };

                // Seleccionar color basado en hash del nombre para consistencia
                var hash = Math.Abs(nombre.GetHashCode());
                var color = colores[hash % colores.Length];

                // Generar URL de avatar con las iniciales
                var avatarUrl = $"https://ui-avatars.com/api/?name={Uri.EscapeDataString(iniciales)}&background={color}&color=fff&size=200&bold=true&format=svg";

                return avatarUrl;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Error al generar avatar, usando avatar por defecto");
                return "/images/default-avatar.png";
            }
        }

        // ✅ MÉTODO PARA OBTENER INICIALES DEL NOMBRE
        private string ObtenerIniciales(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                return "US";

            // Limpiar y dividir el nombre
            var partes = nombre.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (partes.Length == 0)
                return "US";

            if (partes.Length == 1)
            {
                // Un solo nombre: tomar primeras 2 letras
                return partes[0].Length >= 2
                    ? partes[0].Substring(0, 2).ToUpper()
                    : partes[0].ToUpper();
            }
            else
            {
                // Múltiples nombres: tomar primera letra de los primeros 2 nombres
                return $"{partes[0][0]}{partes[1][0]}".ToUpper();
            }
        }

        // ✅ ENDPOINT PARA REGENERAR AVATAR (opcional)
        [HttpPut("{id:int}/regenerar-avatar")]
        [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> RegenerarAvatar(int id)
        {
            try
            {
                var empleado = await _context.Empleados.FindAsync(id);
                if (empleado == null)
                {
                    return NotFound(new { mensaje = "Empleado no encontrado" });
                }

                var nuevoAvatar = GenerarAvatarUrl(empleado.nombre);
                empleado.foto_url = nuevoAvatar;

                await _context.SaveChangesAsync();

                return Ok(new
                {
                    mensaje = "Avatar regenerado exitosamente",
                    avatarUrl = nuevoAvatar
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al regenerar avatar para empleado {Id}", id);
                return StatusCode(500, new { mensaje = "Error al regenerar avatar" });
            }
        }
    }

    // DTOs
    public class EmpleadoCreateDto
    {
        public string CodigoEmpleado { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string FechaNacimiento { get; set; }
        public string FechaIngreso { get; set; }
        public string? FotoUrl { get; set; }
        public int IdDepartamento { get; set; }
        public int IdRol { get; set; }
    }

    public class EmpleadoUpdateDto
    {
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public int IdDepartamento { get; set; }
        public int IdRol { get; set; }
    }
}