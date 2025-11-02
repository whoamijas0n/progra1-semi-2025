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
    public class PermisosController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly ILogger<PermisosController> _logger;

        public PermisosController(MyDbContext context, ILogger<PermisosController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/permisos/mis-permisos → Para empleados
        [HttpGet("mis-permisos")]
        public async Task<IActionResult> GetMisPermisos()
        {
            try
            {
                var empleadoId = User.FindFirst("EmpleadoId")?.Value;
                if (string.IsNullOrEmpty(empleadoId))
                    return Unauthorized();

                var permisos = await _context.Permisos
                    .Where(p => p.idPermisoEmpleado == int.Parse(empleadoId))
                    .OrderByDescending(p => p.fecha_solicitud)
                    .Select(p => new
                    {
                        p.idPermiso,
                        p.tipo,
                        p.fecha_inicio,
                        p.fecha_fin,
                        p.estado,
                        p.motivo,
                        p.fecha_solicitud,
                        supervisor = p.Supervisor != null ? p.Supervisor.Empleado.nombre : (string?)null
                    })
                    .ToListAsync();

                return Ok(permisos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener mis permisos");
                return StatusCode(500, new { mensaje = "Error al cargar permisos" });
            }
        }

        // GET: api/permisos/pending → Para supervisores/admin
        [HttpGet("pending")]
        [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> GetPermisosPendientes()
        {
            try
            {
                var permisos = await _context.Permisos
                    .Include(p => p.Empleado)
                        .ThenInclude(e => e.Departamento)
                    .Where(p => p.estado == "Pendiente")
                    .OrderBy(p => p.fecha_solicitud)
                    .Select(p => new
                    {
                        p.idPermiso,
                        p.tipo,
                        p.fecha_inicio,
                        p.fecha_fin,
                        p.motivo,
                        p.fecha_solicitud,
                        empleado = new
                        {
                            p.Empleado.idEmpleado,
                            p.Empleado.nombre,
                            p.Empleado.codigo_empleado,
                            departamento = p.Empleado.Departamento.nombre
                        }
                    })
                    .ToListAsync();

                return Ok(permisos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener permisos pendientes");
                return StatusCode(500, new { mensaje = "Error al cargar permisos" });
            }
        }

        // GET: api/permisos/historial → Para supervisores/admin
        [HttpGet("historial")]
        [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> GetHistorialPermisos()
        {
            try
            {
                var permisos = await _context.Permisos
                    .Include(p => p.Empleado)
                        .ThenInclude(e => e.Departamento)
                    .Include(p => p.Supervisor)
                        .ThenInclude(s => s.Empleado)
                    .Where(p => p.estado != "Pendiente")
                    .OrderByDescending(p => p.fecha_solicitud)
                    .Select(p => new
                    {
                        p.idPermiso,
                        p.tipo,
                        p.fecha_inicio,
                        p.fecha_fin,
                        p.estado,
                        p.motivo,
                        p.fecha_solicitud,
                        empleado = new
                        {
                            p.Empleado.idEmpleado,
                            p.Empleado.nombre,
                            p.Empleado.codigo_empleado,
                            departamento = p.Empleado.Departamento.nombre
                        },
                        supervisor = p.Supervisor != null ? p.Supervisor.Empleado.nombre : (string?)null
                    })
                    .ToListAsync();

                return Ok(permisos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historial de permisos");
                return StatusCode(500, new { mensaje = "Error al cargar historial" });
            }
        }

        // POST: api/permisos → Crear solicitud (solo empleados)
        [HttpPost]
        public async Task<IActionResult> SolicitarPermiso([FromBody] PermisoCreateDto dto)
        {
            try
            {
                var empleadoId = User.FindFirst("EmpleadoId")?.Value;
                if (string.IsNullOrEmpty(empleadoId))
                    return Unauthorized();

                if (dto.FechaInicio > dto.FechaFin)
                    return BadRequest(new { mensaje = "La fecha de inicio no puede ser mayor que la fecha de fin" });

                var permiso = new Permiso
                {
                    idPermisoEmpleado = int.Parse(empleadoId),
                    idPermisoSupervisor = null, // Pendiente
                    tipo = dto.Tipo.Trim(),
                    fecha_inicio = dto.FechaInicio,
                    fecha_fin = dto.FechaFin,
                    estado = "Pendiente",
                    motivo = dto.Motivo.Trim(),
                    fecha_solicitud = DateTime.Now
                };

                _context.Permisos.Add(permiso);
                await _context.SaveChangesAsync();

                return Ok(new { mensaje = "Solicitud de permiso enviada exitosamente", permiso.idPermiso });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al solicitar permiso");
                return StatusCode(500, new { mensaje = "Error al crear permiso" });
            }
        }

        // PUT: api/permisos/{id}/aprobar → Aprobar (solo supervisor/admin)
        [HttpPut("{id}/aprobar")]
        [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> AprobarPermiso(int id)
        {
            try
            {
                var permiso = await _context.Permisos.FindAsync(id);
                if (permiso == null)
                    return NotFound(new { mensaje = "Permiso no encontrado" });

                if (permiso.estado != "Pendiente")
                    return BadRequest(new { mensaje = "El permiso ya fue procesado" });

                var supervisorId = User.FindFirst("SupervisorId")?.Value;
                if (!string.IsNullOrEmpty(supervisorId))
                {
                    permiso.idPermisoSupervisor = int.Parse(supervisorId);
                }
                // Si es Admin, puede no tener supervisor → se deja como null o se asigna un valor por defecto

                permiso.estado = "Aprobado";
                await _context.SaveChangesAsync();

                return Ok(new { mensaje = "Permiso aprobado exitosamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al aprobar permiso {Id}", id);
                return StatusCode(500, new { mensaje = "Error al aprobar permiso" });
            }
        }

        // PUT: api/permisos/{id}/rechazar → Rechazar (solo supervisor/admin)
        [HttpPut("{id}/rechazar")]
        [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> RechazarPermiso(int id)
        {
            try
            {
                var permiso = await _context.Permisos.FindAsync(id);
                if (permiso == null)
                    return NotFound(new { mensaje = "Permiso no encontrado" });

                if (permiso.estado != "Pendiente")
                    return BadRequest(new { mensaje = "El permiso ya fue procesado" });

                var supervisorId = User.FindFirst("SupervisorId")?.Value;
                if (!string.IsNullOrEmpty(supervisorId))
                {
                    permiso.idPermisoSupervisor = int.Parse(supervisorId);
                }

                permiso.estado = "Rechazado";
                await _context.SaveChangesAsync();

                return Ok(new { mensaje = "Permiso rechazado exitosamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al rechazar permiso {Id}", id);
                return StatusCode(500, new { mensaje = "Error al rechazar permiso" });
            }
        }
    }

    // DTOs
    public class PermisoCreateDto
    {
        [Required, MaxLength(50)]
        public string Tipo { get; set; } = "Ausencia";

        [Required]
        public DateOnly FechaInicio { get; set; }

        [Required]
        public DateOnly FechaFin { get; set; }

        [Required, MaxLength(200)]
        public string Motivo { get; set; } = string.Empty;
    }
}