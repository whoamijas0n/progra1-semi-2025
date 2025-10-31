using fiexpress.Data;
using fiexpress.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fiexpress.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EstadisticasController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly IEstadisticasService _estadisticasService;
        private readonly ILogger<EstadisticasController> _logger;

        public EstadisticasController(MyDbContext context, IEstadisticasService estadisticasService, ILogger<EstadisticasController> logger)
        {
            _context = context;
            _estadisticasService = estadisticasService;
            _logger = logger;
        }

        // GET: api/estadisticas/generales?inicio=2024-01-01&fin=2024-01-31&empleadoId=5
        [HttpGet("generales")]
        [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> GetEstadisticasGenerales(
            [FromQuery] string inicio,
            [FromQuery] string fin,
            [FromQuery] int? empleadoId = null,
            [FromQuery] int? departamentoId = null)
        {
            try
            {
                var fechaInicio = DateOnly.Parse(inicio);
                var fechaFin = DateOnly.Parse(fin);

                // Procesar estadísticas para el rango de fechas
                for (var fecha = fechaInicio; fecha <= fechaFin; fecha = fecha.AddDays(1))
                {
                    await _estadisticasService.ProcesarEstadisticasDelDia(fecha);
                }

                var query = _context.Estadisticas
                    .Include(e => e.Empleado)
                        .ThenInclude(emp => emp.Departamento)
                    .Where(e => e.fecha >= fechaInicio && e.fecha <= fechaFin);

                // Aplicar filtros
                if (empleadoId.HasValue)
                {
                    query = query.Where(e => e.idEmpleadoEstadistica == empleadoId.Value);
                }

                if (departamentoId.HasValue)
                {
                    query = query.Where(e => e.Empleado.idDepartamento == departamentoId.Value);
                }

                var estadisticas = await query
                    .Select(e => new
                    {
                        e.idEstadistica,
                        e.fecha,
                        e.minutos_trabajados,
                        e.minutos_retraso,
                        e.minutos_extra,
                        e.asistencia,
                        e.estado_dia,
                        empleado = new
                        {
                            e.Empleado.idEmpleado,
                            e.Empleado.codigo_empleado,
                            e.Empleado.nombre,
                            e.Empleado.foto_url,
                            departamento = e.Empleado.Departamento.nombre
                        }
                    })
                    .ToListAsync();

                // Agrupar por empleado para resumen
                var resumen = estadisticas
                    .GroupBy(e => e.empleado.idEmpleado)
                    .Select(g => new
                    {
                        empleado = g.First().empleado,
                        diasTrabajados = g.Count(e => e.asistencia),
                        totalDias = g.Count(),
                        totalMinutosTrabajados = g.Sum(e => e.minutos_trabajados ?? 0),
                        totalMinutosRetraso = g.Sum(e => e.minutos_retraso ?? 0),
                        totalMinutosExtra = g.Sum(e => e.minutos_extra ?? 0),
                        porcentajeAsistencia = g.Count() > 0 ? Math.Round((double)g.Count(e => e.asistencia) / g.Count() * 100, 1) : 0,
                        estados = g.GroupBy(e => e.estado_dia)
                            .ToDictionary(x => x.Key, x => x.Count())
                    })
                    .OrderByDescending(r => r.diasTrabajados)
                    .ThenBy(r => r.totalMinutosRetraso)
                    .ToList();

                return Ok(new
                {
                    periodo = $"{fechaInicio:dd/MM/yyyy} - {fechaFin:dd/MM/yyyy}",
                    totalEmpleados = resumen.Count,
                    resumenGeneral = new
                    {
                        totalDiasTrabajados = resumen.Sum(r => r.diasTrabajados),
                        promedioAsistencia = resumen.Average(r => r.porcentajeAsistencia),
                        totalRetraso = resumen.Sum(r => r.totalMinutosRetraso),
                        totalExtra = resumen.Sum(r => r.totalMinutosExtra)
                    },
                    estadisticasDetalladas = resumen,
                    filtrosAplicados = new { empleadoId, departamentoId }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener estadísticas generales");
                return StatusCode(500, new { mensaje = "Error al obtener estadísticas" });
            }
        }

        // GET: api/estadisticas/empleado/{id}?inicio=2024-01-01&fin=2024-01-31
        [HttpGet("empleado/{id}")]
        public async Task<IActionResult> GetEstadisticasEmpleado(int id, [FromQuery] string inicio, [FromQuery] string fin)
        {
            try
            {
                var fechaInicio = DateOnly.Parse(inicio);
                var fechaFin = DateOnly.Parse(fin);

                // Procesar estadísticas para el rango
                for (var fecha = fechaInicio; fecha <= fechaFin; fecha = fecha.AddDays(1))
                {
                    await _estadisticasService.ProcesarEstadisticasDelDia(fecha);
                }

                var estadisticas = await _context.Estadisticas
                    .Include(e => e.Empleado)
                    .Where(e => e.idEmpleadoEstadistica == id && e.fecha >= fechaInicio && e.fecha <= fechaFin)
                    .OrderBy(e => e.fecha)
                    .Select(e => new
                    {
                        e.fecha,
                        e.minutos_trabajados,
                        e.minutos_retraso,
                        e.minutos_extra,
                        e.asistencia,
                        e.estado_dia,
                        horasTrabajadas = e.minutos_trabajados.HasValue ?
                            $"{e.minutos_trabajados.Value / 60}h {e.minutos_trabajados.Value % 60}m" : "0h",
                        retrasoFormateado = e.minutos_retraso.HasValue ?
                            $"{e.minutos_retraso.Value}m" : "0m",
                        extraFormateado = e.minutos_extra.HasValue ?
                            $"{e.minutos_extra.Value}m" : "0m"
                    })
                    .ToListAsync();

                var empleado = await _context.Empleados
                    .Include(e => e.Departamento)
                    .FirstOrDefaultAsync(e => e.idEmpleado == id);

                if (empleado == null)
                {
                    return NotFound(new { mensaje = "Empleado no encontrado" });
                }

                var resumen = new
                {
                    diasTrabajados = estadisticas.Count(e => e.asistencia),
                    totalDias = estadisticas.Count,
                    totalHorasTrabajadas = estadisticas.Sum(e => e.minutos_trabajados ?? 0) / 60,
                    totalMinutosRetraso = estadisticas.Sum(e => e.minutos_retraso ?? 0),
                    totalMinutosExtra = estadisticas.Sum(e => e.minutos_extra ?? 0),
                    porcentajeAsistencia = estadisticas.Count > 0 ?
                        Math.Round((double)estadisticas.Count(e => e.asistencia) / estadisticas.Count * 100, 1) : 0
                };

                return Ok(new
                {
                    empleado = new
                    {
                        empleado.idEmpleado,
                        empleado.nombre,
                        empleado.codigo_empleado,
                        departamento = empleado.Departamento.nombre,
                        empleado.foto_url
                    },
                    periodo = $"{fechaInicio:dd/MM/yyyy} - {fechaFin:dd/MM/yyyy}",
                    resumen,
                    detallePorDia = estadisticas
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener estadísticas del empleado {Id}", id);
                return StatusCode(500, new { mensaje = "Error al obtener estadísticas" });
            }
        }

        // POST: api/estadisticas/procesar-dia
        [HttpPost("procesar-dia")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ProcesarEstadisticasDia([FromQuery] string fecha)
        {
            try
            {
                var fechaProcesar = DateOnly.Parse(fecha);
                await _estadisticasService.ProcesarEstadisticasDelDia(fechaProcesar);

                return Ok(new { mensaje = $"Estadísticas procesadas para {fechaProcesar:dd/MM/yyyy}" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al procesar estadísticas del día");
                return StatusCode(500, new { mensaje = "Error al procesar estadísticas" });
            }
        }
    }
}