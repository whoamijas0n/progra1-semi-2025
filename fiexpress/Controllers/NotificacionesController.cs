using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using fiexpress.Data;
using fiexpress.Models;
using Microsoft.AspNetCore.Authorization;
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

        // GET: api/notificaciones (Para Admin/Supervisor - Todas las notificaciones)
        [HttpGet]
        [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> GetNotificaciones([FromQuery] bool soloNoLeidas = false)
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
                _logger.LogError(ex, "Error al obtener notificaciones");
                return StatusCode(500, new { mensaje = "Error al obtener notificaciones" });
            }
        }

        // GET: api/notificaciones/mis-notificaciones (Para empleados - sus propias notificaciones)
        [HttpGet("mis-notificaciones")]
        public async Task<IActionResult> GetMisNotificaciones([FromQuery] bool soloNoLeidas = false)
        {
            try
            {
                var empleadoId = User.FindFirst("EmpleadoId")?.Value;
                if (string.IsNullOrEmpty(empleadoId))
                {
                    return Unauthorized(new { mensaje = "No se pudo identificar al empleado" });
                }

                var query = _context.Notificaciones
                    .Where(n => n.idNotificacionEmpleado == int.Parse(empleadoId))
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
                        n.fecha_envio
                    })
                    .ToListAsync();

                return Ok(notificaciones);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener mis notificaciones");
                return StatusCode(500, new { mensaje = "Error al obtener notificaciones" });
            }
        }

        // GET: api/notificaciones/estadisticas (Para Admin/Supervisor)
        [HttpGet("estadisticas")]
        [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> GetEstadisticas()
        {
            try
            {
                var totalNotificaciones = await _context.Notificaciones.CountAsync();
                var noLeidas = await _context.Notificaciones.CountAsync(n => !n.leido);
                var leidas = totalNotificaciones - noLeidas;

                var hoy = DateTime.Today;
                var notificacionesHoy = await _context.Notificaciones
                    .CountAsync(n => n.fecha_envio.Date == hoy);

                var notificacionesPorDepartamento = await _context.Notificaciones
                    .Include(n => n.Empleado)
                    .ThenInclude(e => e.Departamento)
                    .GroupBy(n => n.Empleado.Departamento.nombre)
                    .Select(g => new
                    {
                        departamento = g.Key,
                        total = g.Count(),
                        noLeidas = g.Count(n => !n.leido)
                    })
                    .ToListAsync();

                return Ok(new
                {
                    totalNotificaciones,
                    noLeidas,
                    leidas,
                    notificacionesHoy,
                    notificacionesPorDepartamento
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener estadísticas de notificaciones");
                return StatusCode(500, new { mensaje = "Error al obtener estadísticas" });
            }
        }

        // POST: api/notificaciones (Para Admin/Supervisor - Crear notificación)
        [HttpPost]
        [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> Create([FromBody] NotificacionCreateDto dto)
        {
            try
            {
                // ✅ Validar que el empleado exista
                var empleadoExiste = await _context.Empleados
                    .AnyAsync(e => e.idEmpleado == dto.IdEmpleado && e.activo);

                if (!empleadoExiste)
                {
                    return BadRequest(new { mensaje = "El empleado no existe o está inactivo" });
                }

                // ✅ Crear notificación SIN asignar la navegación Empleado
                var notificacion = new Notificacion
                {
                    idNotificacionEmpleado = dto.IdEmpleado,
                    titulo = dto.Titulo?.Trim() ?? string.Empty,
                    mensaje = dto.Mensaje?.Trim() ?? string.Empty,
                    leido = false,
                    fecha_envio = DateTime.Now
                };

                _context.Notificaciones.Add(notificacion);
                await _context.SaveChangesAsync(); // ✅ Esto debería funcionar

                // Registrar auditoría (opcional)
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (int.TryParse(userId, out int usuarioId))
                {
                    var auditoria = new Auditoria
                    {
                        idAuditoriaUsuario = usuarioId,
                        accion = "CREAR_NOTIFICACION",
                        entidad_afectada = "Notificacion",
                        fecha_accion = DateTime.Now,
                        ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown",
                        descripcion = $"Notificación enviada a empleado ID {dto.IdEmpleado}"
                    };
                    _context.Auditorias.Add(auditoria);
                    await _context.SaveChangesAsync();
                }

                return Ok(new { mensaje = "Notificación enviada exitosamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear notificación para empleado {Id}", dto.IdEmpleado);
                return StatusCode(500, new { mensaje = "Error al crear notificación" });
            }
        }

        // POST: api/notificaciones/multiple (Para Admin/Supervisor - Notificación múltiple)
        [HttpPost("multiple")]
        [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> CrearNotificacionMultiple([FromBody] NotificacionMultipleCreateDto dto)
        {
            try
            {
                var notificacionesCreadas = 0;
                var empleadosNoEncontrados = new List<int>();

                foreach (var empleadoId in dto.IdsEmpleados)
                {
                    var empleado = await _context.Empleados
                        .FirstOrDefaultAsync(e => e.idEmpleado == empleadoId && e.activo);

                    if (empleado != null)
                    {
                        var notificacion = new Notificacion
                        {
                            idNotificacionEmpleado = empleadoId,
                            titulo = dto.Titulo.Trim(),
                            mensaje = dto.Mensaje.Trim(),
                            leido = false,
                            fecha_envio = DateTime.Now
                        };

                        _context.Notificaciones.Add(notificacion);
                        notificacionesCreadas++;
                    }
                    else
                    {
                        empleadosNoEncontrados.Add(empleadoId);
                    }
                }

                await _context.SaveChangesAsync();

                // Registrar auditoría
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (int.TryParse(userId, out int usuarioId))
                {
                    var auditoria = new Auditoria
                    {
                        idAuditoriaUsuario = usuarioId,
                        accion = "CREAR_NOTIFICACION_MULTIPLE",
                        entidad_afectada = "Notificacion",
                        fecha_accion = DateTime.Now,
                        ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown",
                        descripcion = $"Notificación múltiple enviada a {notificacionesCreadas} empleados: {dto.Titulo}"
                    };
                    _context.Auditorias.Add(auditoria);
                    await _context.SaveChangesAsync();
                }

                var resultado = new
                {
                    mensaje = $"Notificaciones enviadas exitosamente",
                    notificacionesCreadas,
                    empleadosNoEncontrados = empleadosNoEncontrados.Any() ? empleadosNoEncontrados : null
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear notificaciones múltiples");
                return StatusCode(500, new { mensaje = "Error al crear notificaciones" });
            }
        }

        // PUT: api/notificaciones/{id}/leer (Marcar como leída)
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
                var empleadoId = User.FindFirst("EmpleadoId")?.Value;
                if (string.IsNullOrEmpty(empleadoId) || notificacion.idNotificacionEmpleado != int.Parse(empleadoId))
                {
                    return Forbid("No tienes permiso para modificar esta notificación");
                }

                if (notificacion.leido)
                {
                    return BadRequest(new { mensaje = "La notificación ya está marcada como leída" });
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

        // PUT: api/notificaciones/leer-todas (Marcar todas como leídas)
        [HttpPut("leer-todas")]
        public async Task<IActionResult> MarcarTodasComoLeidas()
        {
            try
            {
                var empleadoId = User.FindFirst("EmpleadoId")?.Value;
                if (string.IsNullOrEmpty(empleadoId))
                {
                    return Unauthorized(new { mensaje = "No se pudo identificar al empleado" });
                }

                var notificaciones = await _context.Notificaciones
                    .Where(n => n.idNotificacionEmpleado == int.Parse(empleadoId) && !n.leido)
                    .ToListAsync();

                foreach (var notificacion in notificaciones)
                {
                    notificacion.leido = true;
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

        // DELETE: api/notificaciones/{id} (Eliminar notificación - Solo Admin/Supervisor)
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> EliminarNotificacion(int id)
        {
            try
            {
                var notificacion = await _context.Notificaciones.FindAsync(id);
                if (notificacion == null)
                {
                    return NotFound(new { mensaje = "Notificación no encontrada" });
                }

                _context.Notificaciones.Remove(notificacion);
                await _context.SaveChangesAsync();

                // Registrar auditoría
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (int.TryParse(userId, out int usuarioId))
                {
                    var auditoria = new Auditoria
                    {
                        idAuditoriaUsuario = usuarioId,
                        accion = "ELIMINAR_NOTIFICACION",
                        entidad_afectada = "Notificacion",
                        fecha_accion = DateTime.Now,
                        ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown",
                        descripcion = $"Notificación eliminada: {notificacion.titulo}"
                    };
                    _context.Auditorias.Add(auditoria);
                    await _context.SaveChangesAsync();
                }

                return Ok(new { mensaje = "Notificación eliminada exitosamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar notificación {Id}", id);
                return StatusCode(500, new { mensaje = "Error al eliminar notificación" });
            }
        }

        // GET: api/notificaciones/contador (Obtener contador de no leídas)
        [HttpGet("contador")]
        public async Task<IActionResult> GetContadorNoLeidas()
        {
            try
            {
                var empleadoId = User.FindFirst("EmpleadoId")?.Value;
                if (string.IsNullOrEmpty(empleadoId))
                {
                    return Unauthorized(new { mensaje = "No se pudo identificar al empleado" });
                }

                var contador = await _context.Notificaciones
                    .CountAsync(n => n.idNotificacionEmpleado == int.Parse(empleadoId) && !n.leido);

                return Ok(new { contadorNoLeidas = contador });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener contador de notificaciones");
                return StatusCode(500, new { mensaje = "Error al obtener contador" });
            }
        }
    }

    // DTOs
    public class NotificacionCreateDto
    {
        public int IdEmpleado { get; set; }
        public string Titulo { get; set; }
        public string Mensaje { get; set; }
    }

    public class NotificacionMultipleCreateDto
    {
        public List<int> IdsEmpleados { get; set; }
        public string Titulo { get; set; }
        public string Mensaje { get; set; }
    }
}