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
    public class IncidenciasController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly ILogger<IncidenciasController> _logger;

        public IncidenciasController(MyDbContext context, ILogger<IncidenciasController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/incidencias/empleado/{idEmpleado}
        [HttpGet("empleado/{idEmpleado}")]
        public async Task<IActionResult> GetByEmpleado(int idEmpleado)
        {
            try
            {
                // Verificar que el usuario actual sea el dueño de las incidencias
                var userId = User.FindFirst("EmpleadoId")?.Value;
                if (string.IsNullOrEmpty(userId) || int.Parse(userId) != idEmpleado)
                {
                    return Forbid(); // Solo el empleado puede ver sus propias incidencias
                }

                var incidencias = await _context.Incidencias
                    .Include(i => i.Supervisor)
                        .ThenInclude(s => s.Empleado)
                    .Where(i => i.idIncidenciaEmpleado == idEmpleado && !i.resuelta)
                    .OrderByDescending(i => i.fecha)
                    .Select(i => new
                    {
                        i.idIncidencia,
                        i.tipo,
                        i.fecha,
                        i.descripcion,
                        resuelta = i.resuelta,
                        fecha_de_resolucion = i.fecha_de_resolucion,
                        supervisor = i.Supervisor != null ? i.Supervisor.Empleado.nombre : (string?)null
                    })
                    .ToListAsync();

                return Ok(incidencias);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener incidencias del empleado {Id}", idEmpleado);
                return StatusCode(500, new { mensaje = "Error al cargar incidencias" });
            }
        }

        // GET: api/incidencias/pending → Para supervisores/admin
        [HttpGet("pending")]
        [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> GetIncidenciasPendientes()
        {
            try
            {
                var incidencias = await _context.Incidencias
                    .Include(i => i.Empleado)
                        .ThenInclude(e => e.Departamento)
                    .Include(i => i.Supervisor)
                        .ThenInclude(s => s.Empleado)
                    .Where(i => !i.resuelta)
                    .OrderBy(i => i.fecha)
                    .Select(i => new
                    {
                        i.idIncidencia,
                        i.tipo,
                        i.fecha,
                        i.descripcion,
                        empleado = new
                        {
                            i.Empleado.idEmpleado,
                            i.Empleado.nombre,
                            i.Empleado.codigo_empleado,
                            departamento = i.Empleado.Departamento.nombre
                        },
                        supervisor = i.Supervisor != null ? i.Supervisor.Empleado.nombre : (string?)null
                    })
                    .ToListAsync();

                return Ok(incidencias);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener incidencias pendientes");
                return StatusCode(500, new { mensaje = "Error al cargar incidencias" });
            }
        }

        // POST: api/incidencias → Crear incidencia (solo empleados)
        [HttpPost]
        public async Task<IActionResult> CrearIncidencia([FromBody] IncidenciaCreateDto dto)
        {
            try
            {
                var empleadoId = User.FindFirst("EmpleadoId")?.Value;
                if (string.IsNullOrEmpty(empleadoId))
                    return Unauthorized();

                // Encontrar supervisor del empleado (debe existir)
                var empleado = await _context.Empleados
                    .Include(e => e.Departamento)
                    .FirstOrDefaultAsync(e => e.idEmpleado == int.Parse(empleadoId));

                if (empleado == null)
                    return BadRequest(new { mensaje = "Empleado no encontrado" });

                var supervisor = await _context.Supervisores
                    .FirstOrDefaultAsync(s => s.idDepartamento_supervisando == empleado.idDepartamento);

                if (supervisor == null)
                    return BadRequest(new { mensaje = "No se encontró un supervisor para tu departamento" });

                var incidencia = new Incidencia
                {
                    idIncidenciaEmpleado = int.Parse(empleadoId),
                    idIncidenciaSupervisor = supervisor.idSupervisor,
                    tipo = dto.Tipo.Trim(),
                    fecha = dto.Fecha,
                    descripcion = dto.Descripcion?.Trim(),
                    resuelta = false,
                    fecha_de_resolucion = null
                };

                _context.Incidencias.Add(incidencia);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    mensaje = "Incidencia registrada exitosamente",
                    incidencia.idIncidencia
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear incidencia");
                return StatusCode(500, new { mensaje = "Error al crear incidencia" });
            }
        }

        // PUT: api/incidencias/{id}/resolver → Marcar como resuelta (solo supervisor/admin)
        [HttpPut("{id}/resolver")]
        [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> ResolverIncidencia(int id)
        {
            try
            {
                var incidencia = await _context.Incidencias.FindAsync(id);
                if (incidencia == null)
                    return NotFound(new { mensaje = "Incidencia no encontrada" });

                if (incidencia.resuelta)
                    return BadRequest(new { mensaje = "La incidencia ya está resuelta" });

                incidencia.resuelta = true;
                incidencia.fecha_de_resolucion = DateOnly.FromDateTime(DateTime.Now);
                await _context.SaveChangesAsync();

                return Ok(new { mensaje = "Incidencia marcada como resuelta" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al resolver incidencia {Id}", id);
                return StatusCode(500, new { mensaje = "Error al resolver incidencia" });
            }
        }
    }

    // DTOs
    public class IncidenciaCreateDto
    {
        [Required, MaxLength(50)]
        public string Tipo { get; set; } = "Retraso";

        [Required]
        public DateOnly Fecha { get; set; }

        [MaxLength(200)]
        public string? Descripcion { get; set; }
    }
}