using fiexpress.Data;
using fiexpress.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace fiexpress.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EmpleadosController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly ILogger<EmpleadosController> _logger;

        public EmpleadosController(MyDbContext context, ILogger<EmpleadosController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/empleados
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] bool incluirInactivos = false,
            [FromQuery] string departamento = null,
            [FromQuery] string estado = null,
            [FromQuery] string rol = null,
            [FromQuery] string buscar = null)
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

                // Aplicar filtros si se proporcionan
                if (!string.IsNullOrEmpty(departamento) && int.TryParse(departamento, out int deptoId))
                {
                    query = query.Where(e => e.idDepartamento == deptoId);
                }

                if (!string.IsNullOrEmpty(estado))
                {
                    query = query.Where(e => e.activo == (estado == "activo"));
                }

                if (!string.IsNullOrEmpty(rol) && int.TryParse(rol, out int rolId))
                {
                    query = query.Where(e => e.idRol == rolId);
                }

                if (!string.IsNullOrEmpty(buscar))
                {
                    query = query.Where(e =>
                        e.nombre.Contains(buscar) ||
                        e.codigo_empleado.Contains(buscar) ||
                        e.email.Contains(buscar));
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
                        e.fecha_ingreso,
                        e.foto_url,
                        departamento = e.Departamento.nombre,
                        rol = e.Rol.nombre
                    })
                    .OrderBy(e => e.nombre)
                    .ToListAsync();

                return Ok(empleados);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener empleados");
                return StatusCode(500, new { mensaje = "Error al obtener empleados" });
            }
        }

        // GET: api/empleados/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var empleado = await _context.Empleados
                    .Include(e => e.Departamento)
                    .Include(e => e.Rol)
                    .Include(e => e.Usuario)
                    .Include(e => e.Supervisor)
                    .FirstOrDefaultAsync(e => e.idEmpleado == id);

                if (empleado == null)
                {
                    return NotFound(new { mensaje = $"Empleado con ID {id} no encontrado" });
                }

                return Ok(new
                {
                    empleado.idEmpleado,
                    empleado.codigo_empleado,
                    empleado.nombre,
                    empleado.email,
                    empleado.telefono,
                    empleado.fecha_nacimiento,
                    empleado.fecha_ingreso,
                    empleado.fecha_baja,
                    empleado.foto_url,
                    empleado.activo,
                    departamento = new
                    {
                        empleado.Departamento.idDepartamento,
                        empleado.Departamento.nombre
                    },
                    rol = new
                    {
                        empleado.Rol.idRol,
                        empleado.Rol.nombre
                    },
                    tieneUsuario = empleado.Usuario != null,
                    esSupervisor = empleado.Supervisor != null
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener empleado {Id}", id);
                return StatusCode(500, new { mensaje = "Error al obtener empleado" });
            }
        }

        // POST: api/empleados
        [HttpPost]
        [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> Create([FromBody] EmpleadoCreateDto dto)
        {
            try
            {
                // Validar que no exista el código de empleado
                if (await _context.Empleados.AnyAsync(e => e.codigo_empleado == dto.CodigoEmpleado))
                {
                    return BadRequest(new { mensaje = "El código de empleado ya existe" });
                }

                // Validar que no exista el email
                if (await _context.Empleados.AnyAsync(e => e.email == dto.Email))
                {
                    return BadRequest(new { mensaje = "El email ya existe" });
                }

                // Validar que el departamento exista
                var departamento = await _context.Departamentos.FindAsync(dto.IdDepartamento);
                if (departamento == null || !departamento.activo)
                {
                    return BadRequest(new { mensaje = "El departamento no existe o está inactivo" });
                }

                // Validar que el rol exista
                var rol = await _context.Roles.FindAsync(dto.IdRol);
                if (rol == null || !rol.activo)
                {
                    return BadRequest(new { mensaje = "El rol no existe o está inactivo" });
                }

                var empleado = new Empleado
                {
                    codigo_empleado = dto.CodigoEmpleado,
                    nombre = dto.Nombre,
                    email = dto.Email,
                    telefono = dto.Telefono,
                    fecha_nacimiento = dto.FechaNacimiento,
                    fecha_ingreso = dto.FechaIngreso ?? DateOnly.FromDateTime(DateTime.Now),
                    foto_url = dto.FotoUrl ?? "/images/default-avatar.png",
                    activo = true,
                    idDepartamento = dto.IdDepartamento,
                    idRol = dto.IdRol
                };

                _context.Empleados.Add(empleado);
                await _context.SaveChangesAsync();

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
                        descripcion = $"Empleado creado: {empleado.nombre} ({empleado.codigo_empleado})"
                    };
                    _context.Auditorias.Add(auditoria);
                    await _context.SaveChangesAsync();
                }

                return CreatedAtAction(nameof(GetById), new { id = empleado.idEmpleado }, new
                {
                    mensaje = "Empleado creado exitosamente",
                    empleado.idEmpleado
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear empleado");
                return StatusCode(500, new { mensaje = "Error al crear empleado" });
            }
        }

        // PUT: api/empleados/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> Update(int id, [FromBody] EmpleadoUpdateDto dto)
        {
            try
            {
                var empleado = await _context.Empleados.FindAsync(id);

                if (empleado == null)
                {
                    return NotFound(new { mensaje = $"Empleado con ID {id} no encontrado" });
                }

                // Validar email único (si cambió)
                if (dto.Email != empleado.email &&
                    await _context.Empleados.AnyAsync(e => e.email == dto.Email && e.idEmpleado != id))
                {
                    return BadRequest(new { mensaje = "El email ya existe" });
                }

                // Validar que el departamento exista
                var departamento = await _context.Departamentos.FindAsync(dto.IdDepartamento);
                if (departamento == null || !departamento.activo)
                {
                    return BadRequest(new { mensaje = "El departamento no existe o está inactivo" });
                }

                // Validar que el rol exista
                var rol = await _context.Roles.FindAsync(dto.IdRol);
                if (rol == null || !rol.activo)
                {
                    return BadRequest(new { mensaje = "El rol no existe o está inactivo" });
                }

                // Guardar datos antiguos para auditoría
                var datosAntiguos = new
                {
                    empleado.nombre,
                    empleado.email,
                    empleado.telefono,
                    empleado.idDepartamento,
                    empleado.idRol
                };

                empleado.nombre = dto.Nombre;
                empleado.email = dto.Email;
                empleado.telefono = dto.Telefono;
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

        // DELETE: api/empleados/{id} (Desactivar, no eliminar)
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var empleado = await _context.Empleados.FindAsync(id);

                if (empleado == null)
                {
                    return NotFound(new { mensaje = $"Empleado con ID {id} no encontrado" });
                }

                if (!empleado.activo)
                {
                    return BadRequest(new { mensaje = "El empleado ya está inactivo" });
                }

                empleado.activo = false;
                empleado.fecha_baja = DateOnly.FromDateTime(DateTime.Now);

                await _context.SaveChangesAsync();

                // Registrar auditoría
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
                    await _context.SaveChangesAsync();
                }

                return Ok(new { mensaje = "Empleado desactivado exitosamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al desactivar empleado {Id}", id);
                return StatusCode(500, new { mensaje = "Error al desactivar empleado" });
            }
        }

        // PUT: api/empleados/{id}/activar
        [HttpPut("{id}/activar")]
        [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> Activar(int id)
        {
            try
            {
                var empleado = await _context.Empleados.FindAsync(id);

                if (empleado == null)
                {
                    return NotFound(new { mensaje = $"Empleado con ID {id} no encontrado" });
                }

                if (empleado.activo)
                {
                    return BadRequest(new { mensaje = "El empleado ya está activo" });
                }

                empleado.activo = true;
                empleado.fecha_baja = null;

                await _context.SaveChangesAsync();

                // Registrar auditoría
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
                    await _context.SaveChangesAsync();
                }

                return Ok(new { mensaje = "Empleado activado exitosamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al activar empleado {Id}", id);
                return StatusCode(500, new { mensaje = "Error al activar empleado" });
            }
        }


        // GET: api/empleados/por-departamento/{idDepartamento}
        [HttpGet("por-departamento/{idDepartamento}")]
        public async Task<IActionResult> GetByDepartamento(int idDepartamento)
        {
            try
            {
                var empleados = await _context.Empleados
                    .Where(e => e.idDepartamento == idDepartamento && e.activo)
                    .Include(e => e.Rol)
                    .Select(e => new
                    {
                        e.idEmpleado,
                        e.codigo_empleado,
                        e.nombre,
                        e.email,
                        rol = e.Rol.nombre
                    })
                    .ToListAsync();

                return Ok(empleados);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener empleados del departamento {Id}", idDepartamento);
                return StatusCode(500, new { mensaje = "Error al obtener empleados" });
            }
        }

        // GET: api/empleados/estadisticas
        [HttpGet("estadisticas")]
        [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> GetEstadisticas()
        {
            try
            {
                var totalEmpleados = await _context.Empleados.CountAsync();
                var empleadosActivos = await _context.Empleados.CountAsync(e => e.activo);
                var empleadosInactivos = totalEmpleados - empleadosActivos;
                var empleadosConUsuario = await _context.Usuarios.CountAsync(u => u.activo);

                var estadisticasPorDepartamento = await _context.Departamentos
                    .Where(d => d.activo)
                    .Select(d => new
                    {
                        departamento = d.nombre,
                        totalEmpleados = d.Empleados.Count(e => e.activo)
                    })
                    .ToListAsync();

                return Ok(new
                {
                    totalEmpleados,
                    empleadosActivos,
                    empleadosInactivos,
                    empleadosConUsuario,
                    estadisticasPorDepartamento
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener estadísticas de empleados");
                return StatusCode(500, new { mensaje = "Error al obtener estadísticas" });
            }
        }
    }

    // DTOs para Empleados
    public class EmpleadoCreateDto
    {
        public string CodigoEmpleado { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public DateOnly FechaNacimiento { get; set; }
        public DateOnly? FechaIngreso { get; set; }
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