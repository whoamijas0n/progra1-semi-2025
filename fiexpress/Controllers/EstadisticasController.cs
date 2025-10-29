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
    public class EstadisticasController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly ILogger<EstadisticasController> _logger;

        public EstadisticasController(MyDbContext context, ILogger<EstadisticasController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/estadisticas/personales
        [HttpGet("personales")]
        public async Task<IActionResult> GetPersonales()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId)) return Unauthorized();

                var usuario = await _context.Usuarios
                    .Include(u => u.Empleado)
                    .FirstOrDefaultAsync(u => u.idUsuario == int.Parse(userId));
                if (usuario == null) return Unauthorized();

                var empleadoId = usuario.Empleado.idEmpleado;
                var hoy = DateOnly.FromDateTime(DateTime.Now);
                var inicioMes = new DateOnly(hoy.Year, hoy.Month, 1);
                var finMes = inicioMes.AddMonths(1).AddDays(-1);

                var estadisticas = await _context.Estadisticas
                    .Where(e => e.idEmpleadoEstadistica == empleadoId && e.fecha >= inicioMes && e.fecha <= finMes)
                    .ToListAsync();

                var diasTrabajados = estadisticas.Count(e => e.asistencia);
                var totalDiasMes = Enumerable.Range(1, finMes.Day)
                    .Select(d => new DateOnly(hoy.Year, hoy.Month, d))
                    .Where(d => d <= hoy)
                    .Count();

                var ultimosFichajes = await _context.Fichajes
                    .Where(f => f.idEmpleadoFichaje == empleadoId)
                    .OrderByDescending(f => f.fecha)
                    .ThenByDescending(f => f.hora)
                    .Take(5)
                    .Select(f => new
                    {
                        f.fecha,
                        hora = f.hora.ToString("HH:mm"),
                        f.tipo,
                        f.observacion
                    })
                    .ToListAsync();

                return Ok(new
                {
                    empleado = new { usuario.Empleado.idEmpleado, usuario.Empleado.nombre },
                    periodo = $"{inicioMes:MMM yyyy}",
                    resumen = new
                    {
                        diasTrabajados,
                        totalDias = totalDiasMes,
                        porcentajeAsistencia = totalDiasMes > 0 ? Math.Round((double)diasTrabajados / totalDiasMes * 100, 1) : 0,
                        minutosRetraso = estadisticas.Sum(e => e.minutos_retraso ?? 0),
                        minutosExtra = estadisticas.Sum(e => e.minutos_extra ?? 0)
                    },
                    ultimosFichajes
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener estadísticas personales");
                return StatusCode(500, new { mensaje = "Error al cargar estadísticas" });
            }
        }

        // GET: api/estadisticas/generales
        [HttpGet("generales")]
        [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> GetGenerales([FromQuery] string periodo = "mes")
        {
            try
            {
                var hoy = DateOnly.FromDateTime(DateTime.Now);
                DateOnly inicio, fin;

                switch (periodo.ToLower())
                {
                    case "semana":
                        var diasHastaLunes = (int)hoy.DayOfWeek == 0 ? 6 : (int)hoy.DayOfWeek - 1;
                        inicio = hoy.AddDays(-diasHastaLunes);
                        fin = inicio.AddDays(6);
                        break;
                    case "mes":
                    default:
                        inicio = new DateOnly(hoy.Year, hoy.Month, 1);
                        fin = inicio.AddMonths(1).AddDays(-1);
                        break;
                }

                var estadisticas = await _context.Estadisticas
                    .Include(e => e.Empleado)
                    .Where(e => e.fecha >= inicio && e.fecha <= fin)
                    .GroupBy(e => new { e.Empleado.idEmpleado, e.Empleado.nombre, e.Empleado.codigo_empleado })
                    .Select(g => new
                    {
                        idEmpleado = g.Key.idEmpleado,
                        nombre = g.Key.nombre,
                        codigo = g.Key.codigo_empleado,
                        diasTrabajados = g.Count(e => e.asistencia),
                        totalDias = (fin.DayNumber - inicio.DayNumber + 1),
                        minutosRetraso = g.Sum(e => e.minutos_retraso ?? 0),
                        minutosExtra = g.Sum(e => e.minutos_extra ?? 0)
                    })
                    .ToListAsync();

                var todosEmpleados = await _context.Empleados
                    .Where(e => e.activo)
                    .Select(e => new { e.idEmpleado, e.nombre, e.codigo_empleado })
                    .ToListAsync();

                var empleadosConEstadisticas = estadisticas.ToDictionary(e => e.idEmpleado);
                var resultadoCompleto = todosEmpleados.Select(emp =>
                {
                    if (empleadosConEstadisticas.TryGetValue(emp.idEmpleado, out var stats))
                        return stats;
                    return new
                    {
                        idEmpleado = emp.idEmpleado,
                        nombre = emp.nombre,
                        codigo = emp.codigo_empleado,
                        diasTrabajados = 0,
                        totalDias = (fin.DayNumber - inicio.DayNumber + 1),
                        minutosRetraso = 0,
                        minutosExtra = 0
                    };
                }).ToList();

                var totalDiasTrabajados = resultadoCompleto.Sum(e => e.diasTrabajados);
                var totalEmpleados = resultadoCompleto.Count;
                var totalDiasLaborables = totalEmpleados * (fin.DayNumber - inicio.DayNumber + 1);
                var porcentajeAsistenciaGeneral = totalDiasLaborables > 0
                    ? Math.Round((double)totalDiasTrabajados / totalDiasLaborables * 100, 1)
                    : 0;

                return Ok(new
                {
                    periodo = $"{inicio:dd/MM/yyyy} - {fin:dd/MM/yyyy}",
                    resumen = new
                    {
                        totalEmpleados,
                        totalDiasTrabajados,
                        porcentajeAsistenciaGeneral,
                        totalMinutosRetraso = resultadoCompleto.Sum(e => e.minutosRetraso),
                        totalMinutosExtra = resultadoCompleto.Sum(e => e.minutosExtra)
                    },
                    empleados = resultadoCompleto
                        .OrderByDescending(e => e.diasTrabajados)
                        .ThenBy(e => e.minutosRetraso)
                        .ToList()
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener estadísticas generales");
                return StatusCode(500, new { mensaje = "Error al cargar estadísticas" });
            }
        }
    }
}