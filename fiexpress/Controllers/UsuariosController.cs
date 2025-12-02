// ========================================
// ARCHIVO: Controllers/UsuariosController.cs
// CRUD de Usuarios
// ========================================
using fiexpress.Data;
using fiexpress.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace fiexpress.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize] // ⚠️ Comentar para desarrollo
    public class UsuariosController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly ILogger<UsuariosController> _logger;

        public UsuariosController(MyDbContext context, ILogger<UsuariosController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/usuarios
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] bool incluirInactivos = false)
        {
            try
            {
                var query = _context.Usuarios
                    .Include(u => u.Empleado)
                        .ThenInclude(e => e.Departamento)
                    .Include(u => u.Empleado)
                        .ThenInclude(e => e.Rol)
                    .AsQueryable();

                if (!incluirInactivos)
                {
                    query = query.Where(u => u.activo);
                }

                var usuarios = await query
                    .Select(u => new
                    {
                        u.idUsuario,
                        u.username,
                        u.activo,
                        u.ultimo_login,
                        empleado = new
                        {
                            u.Empleado.idEmpleado,
                            u.Empleado.nombre,
                            u.Empleado.email,
                            u.Empleado.foto_url,
                            departamento = u.Empleado.Departamento.nombre,
                            rol = u.Empleado.Rol.nombre
                        }
                    })
                    .OrderBy(u => u.username)
                    .ToListAsync();

                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener usuarios");
                return StatusCode(500, new { mensaje = "Error al obtener usuarios" });
            }
        }

        // GET: api/usuarios/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var usuario = await _context.Usuarios
                    .Include(u => u.Empleado)
                        .ThenInclude(e => e.Departamento)
                    .Include(u => u.Empleado)
                        .ThenInclude(e => e.Rol)
                    .FirstOrDefaultAsync(u => u.idUsuario == id);

                if (usuario == null)
                {
                    return NotFound(new { mensaje = $"Usuario con ID {id} no encontrado" });
                }

                return Ok(new
                {
                    usuario.idUsuario,
                    usuario.username,
                    usuario.idUsuarioEmpleado,
                    usuario.activo,
                    usuario.ultimo_login,
                    empleado = new
                    {
                        usuario.Empleado.idEmpleado,
                        usuario.Empleado.nombre,
                        usuario.Empleado.email,
                        usuario.Empleado.telefono,
                        usuario.Empleado.foto_url,
                        departamento = usuario.Empleado.Departamento.nombre,
                        rol = usuario.Empleado.Rol.nombre
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener usuario {Id}", id);
                return StatusCode(500, new { mensaje = "Error al obtener usuario" });
            }
        }

        // GET: api/usuarios/empleados-sin-usuario
        [HttpGet("empleados-sin-usuario")]
        public async Task<IActionResult> GetEmpleadosSinUsuario()
        {
            try
            {
                // Obtener IDs de empleados que YA tienen usuario
                var empleadosConUsuario = await _context.Usuarios
                    .Select(u => u.idUsuarioEmpleado)
                    .ToListAsync();

                // Obtener empleados activos SIN usuario
                var empleados = await _context.Empleados
                    .Include(e => e.Departamento)
                    .Include(e => e.Rol)
                    .Where(e => e.activo && !empleadosConUsuario.Contains(e.idEmpleado))
                    .Select(e => new
                    {
                        e.idEmpleado,
                        e.codigo_empleado,
                        e.nombre,
                        e.email,
                        departamento = e.Departamento.nombre,
                        rol = e.Rol.nombre
                    })
                    .OrderBy(e => e.nombre)
                    .ToListAsync();

                return Ok(empleados);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener empleados sin usuario");
                return StatusCode(500, new { mensaje = "Error al obtener empleados" });
            }
        }

        // POST: api/usuarios
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UsuarioCreateDto dto)
        {
            try
            {
                // Validar que no exista el username
                if (await _context.Usuarios.AnyAsync(u => u.username == dto.Username))
                {
                    return BadRequest(new { mensaje = "El nombre de usuario ya existe" });
                }

                // Validar que el empleado exista y no tenga usuario
                var empleado = await _context.Empleados.FindAsync(dto.IdEmpleado);
                if (empleado == null)
                {
                    return BadRequest(new { mensaje = "El empleado no existe" });
                }

                if (await _context.Usuarios.AnyAsync(u => u.idUsuarioEmpleado == dto.IdEmpleado))
                {
                    return BadRequest(new { mensaje = "El empleado ya tiene un usuario asignado" });
                }

                // TODO: Implementar hash de contraseña (bcrypt, etc.)
                // Por ahora guardamos en texto plano (SOLO PARA DESARROLLO)
                var usuario = new Usuario
                {
                    idUsuarioEmpleado = dto.IdEmpleado,
                    username = dto.Username,
                    password = dto.Password, // ⚠️ Debería estar hasheada
                    activo = true,
                    ultimo_login = null
                };

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetById), new { id = usuario.idUsuario }, new
                {
                    mensaje = "Usuario creado exitosamente",
                    usuario.idUsuario
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear usuario");
                return StatusCode(500, new { mensaje = "Error al crear usuario" });
            }
        }

        // PUT: api/usuarios/{id}
        // PUT: api/usuarios/{id}
        // PUT: api/usuarios/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UsuarioCreateDto dto)
        {
            try
            {
                var usuario = await _context.Usuarios
                    .Include(u => u.Empleado)
                    .FirstOrDefaultAsync(u => u.idUsuario == id);

                if (usuario == null)
                {
                    return NotFound(new { mensaje = $"Usuario con ID {id} no encontrado" });
                }

                // Validar username único (si cambió)
                if (dto.Username != usuario.username &&
                    await _context.Usuarios.AnyAsync(u => u.username == dto.Username && u.idUsuario != id))
                {
                    return BadRequest(new { mensaje = "El nombre de usuario ya existe" });
                }

                // ✅ Validar que el IdEmpleado coincida (no se puede cambiar)
                if (dto.IdEmpleado != usuario.idUsuarioEmpleado)
                {
                    return BadRequest(new { mensaje = "No se puede cambiar el empleado asignado al usuario" });
                }

                // ✅ Actualizar todos los campos
                usuario.username = dto.Username;
                if (!string.IsNullOrWhiteSpace(dto.Password))
                {
                    // TODO: Hashear en producción
                    usuario.password = dto.Password;
                }
                usuario.activo = true; // o usa dto.Activo si lo incluyes

                await _context.SaveChangesAsync();
                return Ok(new { mensaje = "Usuario actualizado exitosamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar usuario {Id}", id);
                return StatusCode(500, new { mensaje = "Error al actualizar usuario" });
            }
        }

        // PUT: api/usuarios/{id}/cambiar-password
        [HttpPut("{id}/cambiar-password")]
        public async Task<IActionResult> CambiarPassword(int id, [FromBody] CambiarPasswordDto dto)
        {
            try
            {
                var usuario = await _context.Usuarios.FindAsync(id);

                if (usuario == null)
                {
                    return NotFound(new { mensaje = $"Usuario con ID {id} no encontrado" });
                }

                // TODO: Validar contraseña actual y hashear la nueva
                usuario.password = dto.NuevaPassword;

                await _context.SaveChangesAsync();

                return Ok(new { mensaje = "Contraseña actualizada exitosamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cambiar contraseña usuario {Id}", id);
                return StatusCode(500, new { mensaje = "Error al cambiar contraseña" });
            }
        }

        // DELETE: api/usuarios/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var usuario = await _context.Usuarios.FindAsync(id);

                if (usuario == null)
                {
                    return NotFound(new { mensaje = $"Usuario con ID {id} no encontrado" });
                }

                // Desactivar en lugar de eliminar
                usuario.activo = false;
                await _context.SaveChangesAsync();

                return Ok(new { mensaje = "Usuario desactivado exitosamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al desactivar usuario {Id}", id);
                return StatusCode(500, new { mensaje = "Error al desactivar usuario" });
            }
        }

        // PUT: api/usuarios/{id}/activar
        [HttpPut("{id}/activar")]
        public async Task<IActionResult> Activar(int id)
        {
            try
            {
                var usuario = await _context.Usuarios.FindAsync(id);

                if (usuario == null)
                {
                    return NotFound(new { mensaje = $"Usuario con ID {id} no encontrado" });
                }

                usuario.activo = true;
                await _context.SaveChangesAsync();

                return Ok(new { mensaje = "Usuario activado exitosamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al activar usuario {Id}", id);
                return StatusCode(500, new { mensaje = "Error al activar usuario" });
            }
        }
    }

    // DTOs
    public class UsuarioCreateDto
    {
        [Required]
        public int IdEmpleado { get; set; }

        [Required, MaxLength(50)]
        public string Username { get; set; }

        [Required, MinLength(6), MaxLength(35)]
        public string Password { get; set; }
    }

    public class UsuarioUpdateDto
    {
        [Required, MaxLength(50)]
        public string Username { get; set; }

        [MaxLength(35)]
        public string Password { get; set; } // Opcional al editar

        public bool Activo { get; set; }
    }

    public class CambiarPasswordDto
    {
        [Required, MinLength(6), MaxLength(35)]
        public string NuevaPassword { get; set; }
    }
}