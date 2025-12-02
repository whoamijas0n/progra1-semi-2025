// ========================================
// ARCHIVO: Controllers/RolesController.cs
// CRUD de Roles
// ========================================
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
    public class RolesController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly ILogger<RolesController> _logger;

        public RolesController(MyDbContext context, ILogger<RolesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/roles
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] bool incluirInactivos = false)
        {
            try
            {
                var query = _context.Roles.AsQueryable();

                if (!incluirInactivos)
                {
                    query = query.Where(r => r.activo);
                }

                var roles = await query
                    .OrderBy(r => r.nombre)
                    .ToListAsync();

                return Ok(roles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener roles");
                return StatusCode(500, new { mensaje = "Error al obtener roles" });
            }
        }

        // GET: api/roles/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var rol = await _context.Roles.FindAsync(id);

                if (rol == null)
                {
                    return NotFound(new { mensaje = $"Rol con ID {id} no encontrado" });
                }

                return Ok(rol);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener rol {Id}", id);
                return StatusCode(500, new { mensaje = "Error al obtener rol" });
            }
        }

        // POST: api/roles
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] RolDto dto)
        {
            try
            {
                // Validar que no exista el nombre
                if (await _context.Roles.AnyAsync(r => r.nombre == dto.Nombre))
                {
                    return BadRequest(new { mensaje = "El nombre del rol ya existe" });
                }

                var rol = new Rol
                {
                    nombre = dto.Nombre,
                    descripcion = dto.Descripcion,
                    activo = true
                };

                _context.Roles.Add(rol);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetById), new { id = rol.idRol }, new
                {
                    mensaje = "Rol creado exitosamente",
                    rol.idRol
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear rol");
                return StatusCode(500, new { mensaje = "Error al crear rol" });
            }
        }

        // PUT: api/roles/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] RolDto dto)
        {
            try
            {
                var rol = await _context.Roles.FindAsync(id);

                if (rol == null)
                {
                    return NotFound(new { mensaje = $"Rol con ID {id} no encontrado" });
                }

                // Validar nombre único (si cambió)
                if (dto.Nombre != rol.nombre &&
                    await _context.Roles.AnyAsync(r => r.nombre == dto.Nombre && r.idRol != id))
                {
                    return BadRequest(new { mensaje = "El nombre del rol ya existe" });
                }

                rol.nombre = dto.Nombre;
                rol.descripcion = dto.Descripcion;

                await _context.SaveChangesAsync();

                return Ok(new { mensaje = "Rol actualizado exitosamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar rol {Id}", id);
                return StatusCode(500, new { mensaje = "Error al actualizar rol" });
            }
        }


        // PUT: api/roles/{id}/activar
        [HttpPut("{id}/activar")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Activar(int id)
        {
            try
            {
                var rol = await _context.Roles.FindAsync(id);

                if (rol == null)
                {
                    return NotFound(new { mensaje = $"Rol con ID {id} no encontrado" });
                }

                if (rol.activo)
                {
                    return BadRequest(new { mensaje = "El rol ya está activo" });
                }

                rol.activo = true;
                await _context.SaveChangesAsync();

                // Registrar auditoría
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (int.TryParse(userId, out int usuarioId))
                {
                    var auditoria = new Auditoria
                    {
                        idAuditoriaUsuario = usuarioId,
                        accion = "ACTIVAR_ROL",
                        entidad_afectada = "Rol",
                        fecha_accion = DateTime.Now,
                        ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown",
                        descripcion = $"Rol activado: {rol.nombre}"
                    };
                    _context.Auditorias.Add(auditoria);
                    await _context.SaveChangesAsync();
                }

                return Ok(new { mensaje = "Rol activado exitosamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al activar rol {Id}", id);
                return StatusCode(500, new { mensaje = "Error al activar rol" });
            }
        }

        // DELETE: api/roles/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var rol = await _context.Roles.FindAsync(id);

                if (rol == null)
                {
                    return NotFound(new { mensaje = $"Rol con ID {id} no encontrado" });
                }

                // Verificar si tiene empleados asociados
                var tieneEmpleados = await _context.Empleados.AnyAsync(e => e.idRol == id && e.activo);

                if (tieneEmpleados)
                {
                    return BadRequest(new { mensaje = "No se puede desactivar un rol con empleados activos" });
                }

                rol.activo = false;
                await _context.SaveChangesAsync();

                return Ok(new { mensaje = "Rol desactivado exitosamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al desactivar rol {Id}", id);
                return StatusCode(500, new { mensaje = "Error al desactivar rol" });
            }
        }
    }

    // DTO para Roles
    public class RolDto
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}