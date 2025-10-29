// ========================================
// ARCHIVO: Controllers/NotificacionesController.cs
// Gestión de Notificaciones para Empleados y Supervisores
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
    public class NotificacionesController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly ILogger<NotificacionesController> _logger;

        public NotificacionesController(MyDbContext context, ILogger<NotificacionesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/notificaciones
        // Devuelve todas las notificaciones (solo para Admin/Supervisor)
        [HttpGet]
        [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> GetAll([FromQuery] bool soloNoLeidas = false)
        {
            try
            {
                var query = _context.Notificaciones
                    .Include(n => n.Empleado)
                        .ThenInclude(e => e.Departamento)
                    .AsQueryable();

                if (soloNoLeidas)
                {
                    query = query.Where(n => !n.leido);
                }

                var notificaciones = await query
                    .OrderByDescending(n => n.fecha_envio)
                    .Select(n => new
                    {
                        n.idNotificacion,
                        n.titulo,
                        n.mensaje,
                        n.leido,
                        n.fecha_envio,
                        empleado = new
                        {
                            n.Empleado.idEmpleado,
                            n.Empleado.nombre,
                            n.Empleado.codigo_empleado,
                            departamento = n.Empleado.Departamento.nombre
                        }
                    })
                    .ToListAsync();

                return Ok(notificaciones);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas las notificaciones");
                return StatusCode(500, new { mensaje = "Error al obtener notificaciones" });
            }
        }

        // GET: api/notificaciones/empleado/{idEmpleado}
        // Solo el empleado puede ver sus propias notificaciones
        [HttpGet("empleado/{idEmpleado}")]
        public async Task<IActionResult> GetByEmpleado(int idEmpleado)
        {
            try
            {
                // Verificar que el usuario actual sea el dueño de las notificaciones
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                var usuario = await _context.Usuarios
                    .Include(u => u.Empleado)
                    .FirstOrDefaultAsync(u => u.idUsuario == int.Parse(userId));

                if (usuario == null)
                    return Unauthorized();

                bool esAdminOSupervisor = usuario.Empleado.Rol.nombre == "Admin" ||
                                          usuario.Empleado.Supervisor != null;

                // Si no es admin/supervisor, solo puede ver sus propias notificaciones
                if (!esAdminOSupervisor && usuario.Empleado.idEmpleado != idEmpleado)
                {
                    return Forbid();
                }

                var notificaciones = await _context.Notificaciones
                    .Where(n => n.idNotificacionEmpleado == idEmpleado)
                    .OrderByDescending(n => n.fecha_envio)
                    .Select(n => new
                    {
                        n.idNotificacion,
                        n.titulo,
                        n.mensaje,
                        n.leido,
                        n.fecha_envio
                    })
                    .ToListAsync();

                return Ok(notificaciones);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener notificaciones del empleado {Id}", idEmpleado);
                return StatusCode(500, new { mensaje = "Error al obtener notificaciones" });
            }
        }

        // POST: api/notificaciones
        // Crear una notificación (solo Admin/Supervisor)
        [HttpPost]
        [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> Create([FromBody] NotificacionCreateDto dto)
        {
            try
            {
                // Validar que el empleado exista
                var empleado = await _context.Empleados.FindAsync(dto.IdEmpleado);
                if (empleado == null || !empleado.activo)
                {
                    return BadRequest(new { mensaje = "El empleado no existe o está inactivo" });
                }

                var notificacion = new Notificacion
                {
                    idNotificacionEmpleado = dto.IdEmpleado,
                    titulo = dto.Titulo.Trim(),
                    mensaje = dto.Mensaje.Trim(),
                    leido = false,
                    fecha_envio = DateTime.Now
                };

                _context.Notificaciones.Add(notificacion);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetByEmpleado), new { idEmpleado = dto.IdEmpleado }, new
                {
                    mensaje = "Notificación enviada exitosamente",
                    notificacion.idNotificacion
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear notificación");
                return StatusCode(500, new { mensaje = "Error al crear notificación" });
            }
        }

        // PUT: api/notificaciones/{id}/leer
        // Marcar como leída (cualquier usuario autenticado)
        [HttpPut("{id}/leer")]
        public async Task<IActionResult> MarcarComoLeida(int id)
        {
            try
            {
                var notificacion = await _context.Notificaciones.FindAsync(id);
                if (notificacion == null)
                {
                    return NotFound(new { mensaje = "Notificación no encontrada" });
                }

                // Verificar que el usuario actual sea el destinatario
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                var usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.idUsuario == int.Parse(userId));

                if (usuario == null || usuario.Empleado.idEmpleado != notificacion.idNotificacionEmpleado)
                {
                    return Forbid();
                }

                notificacion.leido = true;
                await _context.SaveChangesAsync();

                return Ok(new { mensaje = "Notificación marcada como leída" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al marcar notificación como leída {Id}", id);
                return StatusCode(500, new { mensaje = "Error al actualizar notificación" });
            }
        }

        // PUT: api/notificaciones/leer-todas
        // Marcar todas como leídas para el empleado actual
        [HttpPut("leer-todas")]
        public async Task<IActionResult> MarcarTodasComoLeidas()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                var usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.idUsuario == int.Parse(userId));

                if (usuario == null)
                    return Unauthorized();

                var notificaciones = await _context.Notificaciones
                    .Where(n => n.idNotificacionEmpleado == usuario.Empleado.idEmpleado && !n.leido)
                    .ToListAsync();

                foreach (var n in notificaciones)
                {
                    n.leido = true;
                }

                await _context.SaveChangesAsync();

                return Ok(new { mensaje = $"Se marcaron {notificaciones.Count} notificaciones como leídas" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al marcar todas las notificaciones como leídas");
                return StatusCode(500, new { mensaje = "Error al actualizar notificaciones" });
            }
        }
    }

    // DTO para crear notificaciones
    public class NotificacionCreateDto
    {
        public int IdEmpleado { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Mensaje { get; set; } = string.Empty;
    }
}