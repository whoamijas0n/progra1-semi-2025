using fiexpress.Data;
using fiexpress.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace fiexpress.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class JustificacionesController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly ILogger<JustificacionesController> _logger;

        public JustificacionesController(MyDbContext context, ILogger<JustificacionesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/justificaciones/mis-justificaciones
        [HttpGet("mis-justificaciones")]
        public async Task<IActionResult> GetMisJustificaciones()
        {
            try
            {
                var empleadoId = User.FindFirst("EmpleadoId")?.Value;
                if (string.IsNullOrEmpty(empleadoId))
                    return Unauthorized();

                var justificaciones = await _context.Justificaciones
                    .Include(j => j.Supervisor)
                        .ThenInclude(s => s.Empleado)
                    .Where(j => j.idJustificacionEmpleado == int.Parse(empleadoId))
                    .OrderByDescending(j => j.idJustificacion)
                    .Select(j => new
                    {
                        j.idJustificacion,
                        j.motivo,
                        j.estado,
                        j.documento_url,
                        j.fecha_revision,
                        supervisor = j.Supervisor != null ? j.Supervisor.Empleado.nombre : "Pendiente"
                    })
                    .ToListAsync();

                return Ok(justificaciones);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener mis justificaciones");
                return StatusCode(500, new { mensaje = "Error al cargar justificaciones" });
            }
        }

        // GET: api/justificaciones/pending
        [HttpGet("pending")]
        [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> GetJustificacionesPendientes()
        {
            try
            {
                var justificaciones = await _context.Justificaciones
                    .Include(j => j.Empleado)
                        .ThenInclude(e => e.Departamento)
                    .Include(j => j.Supervisor)
                    .Where(j => j.estado == "Pendiente")
                    .OrderBy(j => j.idJustificacion)
                    .Select(j => new
                    {
                        j.idJustificacion,
                        j.motivo,
                        j.documento_url,
                        empleado = new
                        {
                            j.Empleado.idEmpleado,
                            j.Empleado.nombre,
                            j.Empleado.codigo_empleado,
                            departamento = j.Empleado.Departamento.nombre
                        }
                    })
                    .ToListAsync();

                return Ok(justificaciones);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener justificaciones pendientes");
                return StatusCode(500, new { mensaje = "Error al cargar justificaciones" });
            }
        }

        // GET: api/justificaciones/historial
        [HttpGet("historial")]
        [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> GetHistorialJustificaciones()
        {
            try
            {
                var justificaciones = await _context.Justificaciones
                    .Include(j => j.Empleado)
                        .ThenInclude(e => e.Departamento)
                    .Include(j => j.Supervisor)
                        .ThenInclude(s => s.Empleado)
                    .Where(j => j.estado != "Pendiente")
                    .OrderByDescending(j => j.idJustificacion)
                    .Select(j => new
                    {
                        j.idJustificacion,
                        j.motivo,
                        j.estado,
                        j.documento_url,
                        j.fecha_revision,
                        empleado = new
                        {
                            j.Empleado.idEmpleado,
                            j.Empleado.nombre,
                            j.Empleado.codigo_empleado,
                            departamento = j.Empleado.Departamento.nombre
                        },
                        supervisor = j.Supervisor != null ? j.Supervisor.Empleado.nombre : (string?)null
                    })
                    .ToListAsync();

                return Ok(justificaciones);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historial de justificaciones");
                return StatusCode(500, new { mensaje = "Error al cargar historial" });
            }
        }

        // POST: api/justificaciones
        [HttpPost]
        public async Task<IActionResult> CrearJustificacion([FromBody] JustificacionCreateDto dto)
        {
            try
            {
                var empleadoId = User.FindFirst("EmpleadoId")?.Value;
                if (string.IsNullOrEmpty(empleadoId))
                    return Unauthorized();

                // Encontrar supervisor del empleado
                var empleado = await _context.Empleados
                    .Include(e => e.Departamento)
                    .FirstOrDefaultAsync(e => e.idEmpleado == int.Parse(empleadoId));

                if (empleado == null)
                    return BadRequest(new { mensaje = "Empleado no encontrado" });

                var supervisor = await _context.Supervisores
                    .FirstOrDefaultAsync(s => s.idDepartamento_supervisando == empleado.idDepartamento);

                var justificacion = new Justificacion
                {
                    idJustificacionEmpleado = int.Parse(empleadoId),
                    idJustificacionSupervisor = supervisor?.idSupervisor,
                    motivo = dto.Motivo.Trim(),
                    documento_url = string.IsNullOrWhiteSpace(dto.DocumentoUrl) ? null : dto.DocumentoUrl.Trim(),
                    estado = "Pendiente",
                    fecha_revision = null
                };

                _context.Justificaciones.Add(justificacion);
                await _context.SaveChangesAsync();

                return Ok(new { mensaje = "Justificación enviada exitosamente", justificacion.idJustificacion });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear justificación");
                return StatusCode(500, new { mensaje = "Error al crear justificación" });
            }
        }

        // PUT: api/justificaciones/{id}/aprobar
        [HttpPut("{id}/aprobar")]
        [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> AprobarJustificacion(int id)
        {
            try
            {
                var justificacion = await _context.Justificaciones.FindAsync(id);
                if (justificacion == null)
                    return NotFound(new { mensaje = "Justificación no encontrada" });

                if (justificacion.estado != "Pendiente")
                    return BadRequest(new { mensaje = "La justificación ya fue procesada" });

                var supervisorId = User.FindFirst("SupervisorId")?.Value;
                if (!string.IsNullOrEmpty(supervisorId))
                {
                    justificacion.idJustificacionSupervisor = int.Parse(supervisorId);
                }

                justificacion.estado = "Aprobado";
                justificacion.fecha_revision = DateTime.Now;
                await _context.SaveChangesAsync();

                return Ok(new { mensaje = "Justificación aprobada exitosamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al aprobar justificación {Id}", id);
                return StatusCode(500, new { mensaje = "Error al aprobar justificación" });
            }
        }

        // PUT: api/justificaciones/{id}/rechazar
        [HttpPut("{id}/rechazar")]
        [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> RechazarJustificacion(int id)
        {
            try
            {
                var justificacion = await _context.Justificaciones.FindAsync(id);
                if (justificacion == null)
                    return NotFound(new { mensaje = "Justificación no encontrada" });

                if (justificacion.estado != "Pendiente")
                    return BadRequest(new { mensaje = "La justificación ya fue procesada" });

                var supervisorId = User.FindFirst("SupervisorId")?.Value;
                if (!string.IsNullOrEmpty(supervisorId))
                {
                    justificacion.idJustificacionSupervisor = int.Parse(supervisorId);
                }

                justificacion.estado = "Rechazado";
                justificacion.fecha_revision = DateTime.Now;
                await _context.SaveChangesAsync();

                return Ok(new { mensaje = "Justificación rechazada exitosamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al rechazar justificación {Id}", id);
                return StatusCode(500, new { mensaje = "Error al rechazar justificación" });
            }
        }
    }

    public class JustificacionCreateDto
    {
        [Required, MaxLength(200)]
        public string Motivo { get; set; } = string.Empty;

        [MaxLength(150)]
        public string? DocumentoUrl { get; set; }
    }
}