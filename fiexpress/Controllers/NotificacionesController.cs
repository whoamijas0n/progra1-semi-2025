using fiexpress.Data;
using fiexpress.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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
        [HttpGet]
        [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> GetNotificaciones([FromQuery] bool soloNoLeidas = false, [FromQuery] int? empleadoId = null)
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

                if (empleadoId.HasValue)
                {
                    query = query.Where(n => n.idNotificacionEmpleado == empleadoId.Value);
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

        // GET: api/notificaciones/mis-notificaciones
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

        // GET: api/notificaciones/estadisticas
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

                return Ok(new
                {
                    totalNotificaciones,
                    noLeidas,
                    leidas,
                    notificacionesHoy
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener estadísticas de notificaciones");
                return StatusCode(500, new { mensaje = "Error al obtener estadísticas" });
            }
        }
        // POST: api/notificaciones/fallback
        [HttpPost("fallback")]
        [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> CreateFallback([FromBody] NotificacionCreateDto dto)
        {
            try
            {
                // Validaciones
                if (dto.IdEmpleado <= 0 || string.IsNullOrWhiteSpace(dto.Titulo) || string.IsNullOrWhiteSpace(dto.Mensaje))
                {
                    return BadRequest(new { mensaje = "Datos incompletos" });
                }

                // Verificar empleado
                var empleadoExiste = await _context.Empleados
                    .AnyAsync(e => e.idEmpleado == dto.IdEmpleado && e.activo);

                if (!empleadoExiste)
                {
                    return BadRequest(new { mensaje = "Empleado no encontrado" });
                }

                // ✅ SQL DIRECTO para evitar problemas con EF
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                using var command = connection.CreateCommand();
                command.CommandText = @"
            INSERT INTO notificacion (idNotificacionEmpleado, titulo, mensaje, leido, fecha_envio)
            VALUES (@empleadoId, @titulo, @mensaje, @leido, @fecha);
            SELECT SCOPE_IDENTITY();";

                command.Parameters.Add(new SqlParameter("@empleadoId", dto.IdEmpleado));
                command.Parameters.Add(new SqlParameter("@titulo", dto.Titulo.Trim()));
                command.Parameters.Add(new SqlParameter("@mensaje", dto.Mensaje.Trim()));
                command.Parameters.Add(new SqlParameter("@leido", false)); // ✅ False explícito
                command.Parameters.Add(new SqlParameter("@fecha", DateTime.Now));

                var nuevoId = await command.ExecuteScalarAsync();

                _logger.LogInformation("✅ Notificación creada con SQL directo. ID: {Id}", nuevoId);

                return Ok(new
                {
                    mensaje = "Notificación enviada exitosamente",
                    id = Convert.ToInt32(nuevoId)
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Error en fallback");
                return StatusCode(500, new
                {
                    mensaje = "Error al crear notificación",
                    error = ex.Message
                });
            }
        }


        // POST: api/notificaciones/multiple
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

        // PUT: api/notificaciones/{id}/leer
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

        // PUT: api/notificaciones/leer-todas
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

        // DELETE: api/notificaciones/{id}
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
    }

    // DTOs - Asegúrate de que coincidan con lo que envían tus vistas
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