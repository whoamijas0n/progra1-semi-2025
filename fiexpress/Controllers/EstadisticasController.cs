using fiexpress.Data;
using fiexpress.Models;
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

        // Agregar en EstadisticasController.cs
        [HttpGet("dashboard-empleado")]
        public async Task<IActionResult> GetDashboardEmpleado()
        {
            try
            {
                var empleadoId = User.FindFirst("EmpleadoId")?.Value;
                if (string.IsNullOrEmpty(empleadoId))
                    return Unauthorized(new { mensaje = "No se pudo identificar al empleado" });

                var hoy = DateOnly.FromDateTime(DateTime.Today);
                var inicioMes = new DateOnly(hoy.Year, hoy.Month, 1);

                // 1. Datos del empleado
                var empleado = await _context.Empleados
                    .Include(e => e.Departamento)
                    .FirstOrDefaultAsync(e => e.idEmpleado == int.Parse(empleadoId));

                if (empleado == null)
                    return NotFound(new { mensaje = "Empleado no encontrado" });

                // 2. Fichajes de hoy
                var fichajesHoy = await _context.Fichajes
                    .Where(f => f.idEmpleadoFichaje == int.Parse(empleadoId) && f.fecha == hoy)
                    .OrderByDescending(f => f.hora)
                    .Select(f => new
                    {
                        f.idFichaje,
                        f.tipo,
                        hora = f.hora.ToString("HH:mm"),
                        fecha = f.fecha.ToString("yyyy-MM-dd")
                    })
                    .ToListAsync();

                // 3. Horario activo (CORREGIDO - mejor consulta)
                var horarioActivo = await _context.Horarios
                    .Include(h => h.Turno)
                    .Where(h => h.idHorario_De_Empleado == int.Parse(empleadoId)
                        && h.activo
                        && h.fecha_inicio <= hoy
                        && (h.fecha_fin == null || h.fecha_fin >= hoy))
                    .OrderByDescending(h => h.fecha_inicio) // Tomar el más reciente
                    .Select(h => new
                    {
                        idHorario = h.idHorario,
                        nombreTurno = h.Turno.nombre,
                        horaEntrada = h.Turno.hora_entrada.ToString("HH:mm"),
                        horaSalida = h.Turno.hora_salida.ToString("HH:mm"),
                        toleranciaMinutos = h.Turno.tolerancia_minutos,
                        diasActivos = new
                        {
                            lunes = h.Turno.lunes,
                            martes = h.Turno.martes,
                            miercoles = h.Turno.miercoles,
                            jueves = h.Turno.jueves,
                            viernes = h.Turno.viernes,
                            sabado = h.Turno.sabado,
                            domingo = h.Turno.domingo
                        }
                    })
                    .FirstOrDefaultAsync();

                // 4. Estadísticas del mes actual (PROCESAR SI NO EXISTEN)
                // Procesar estadísticas para asegurar datos actualizados
                for (var fecha = inicioMes; fecha <= hoy; fecha = fecha.AddDays(1))
                {
                    await _estadisticasService.ProcesarEstadisticasDelDia(fecha);
                }

                var estadisticasMes = await _context.Estadisticas
                    .Where(e => e.idEmpleadoEstadistica == int.Parse(empleadoId)
                        && e.fecha >= inicioMes
                        && e.fecha <= hoy)
                    .ToListAsync();

                // 5. Calcular resumen
                var resumenMes = new
                {
                    totalDias = estadisticasMes.Count,
                    diasTrabajados = estadisticasMes.Count(e => e.asistencia),
                    totalMinutosTrabajados = estadisticasMes.Sum(e => e.minutos_trabajados ?? 0),
                    totalMinutosRetraso = estadisticasMes.Sum(e => e.minutos_retraso ?? 0),
                    totalMinutosExtra = estadisticasMes.Sum(e => e.minutos_extra ?? 0),
                    porcentajeAsistencia = estadisticasMes.Count > 0 ?
                        Math.Round((double)estadisticasMes.Count(e => e.asistencia) / estadisticasMes.Count * 100, 1) : 0
                };

                // 6. Estado actual del empleado
                var estadoActual = "Sin fichajes hoy";
                var ultimoFichaje = fichajesHoy.FirstOrDefault();
                if (ultimoFichaje != null)
                {
                    estadoActual = ultimoFichaje.tipo == "Entrada" ? "En trabajo" : "Fuera de oficina";
                }

                return Ok(new
                {
                    empleado = new
                    {
                        id = empleado.idEmpleado,
                        nombre = empleado.nombre,
                        codigo_empleado = empleado.codigo_empleado,
                        email = empleado.email,
                        foto_url = empleado.foto_url,
                        departamento = empleado.Departamento.nombre,
                        activo = empleado.activo
                    },
                    fichajesHoy,
                    horarioActivo,
                    resumenMes,
                    estadoActual,
                    ultimoFichaje,
                    timestamp = DateTime.Now
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener dashboard del empleado");
                return StatusCode(500, new { mensaje = "Error al obtener datos del dashboard" });
            }
        }


        // Agregar en EstadisticasController.cs
        [HttpGet("mis-estadisticas-detalladas")]
        public async Task<IActionResult> GetMisEstadisticasDetalladas([FromQuery] string inicio, [FromQuery] string fin)
        {
            try
            {
                var empleadoId = User.FindFirst("EmpleadoId")?.Value;
                if (string.IsNullOrEmpty(empleadoId))
                    return Unauthorized(new { mensaje = "No se pudo identificar al empleado" });

                var fechaInicio = DateOnly.Parse(inicio);
                var fechaFin = DateOnly.Parse(fin);

                // Procesar estadísticas para el rango
                for (var fecha = fechaInicio; fecha <= fechaFin; fecha = fecha.AddDays(1))
                {
                    await _estadisticasService.ProcesarEstadisticasDelDia(fecha);
                }

                // Obtener estadísticas
                var estadisticas = await _context.Estadisticas
                    .Include(e => e.Empleado)
                    .Where(e => e.idEmpleadoEstadistica == int.Parse(empleadoId) &&
                               e.fecha >= fechaInicio && e.fecha <= fechaFin)
                    .OrderBy(e => e.fecha)
                    .ToListAsync();

                // Obtener fichajes para cálculos adicionales
                var fichajes = await _context.Fichajes
                    .Where(f => f.idEmpleadoFichaje == int.Parse(empleadoId) &&
                               f.fecha >= fechaInicio && f.fecha <= fechaFin)
                    .OrderBy(f => f.fecha)
                    .ThenBy(f => f.hora)
                    .ToListAsync();

                // Calcular métricas avanzadas
                var metricas = CalcularMetricasAvanzadas(estadisticas, fichajes);

                var empleado = await _context.Empleados
                    .Include(e => e.Departamento)
                    .FirstOrDefaultAsync(e => e.idEmpleado == int.Parse(empleadoId));

                if (empleado == null)
                {
                    return NotFound(new { mensaje = "Empleado no encontrado" });
                }

                return Ok(new
                {
                    empleado = new
                    {
                        empleado.idEmpleado,
                        empleado.nombre,
                        empleado.codigo_empleado,
                        empleado.foto_url,
                        departamento = empleado.Departamento.nombre
                    },
                    periodo = $"{fechaInicio:dd/MM/yyyy} - {fechaFin:dd/MM/yyyy}",
                    resumen = new
                    {
                        totalDias = estadisticas.Count,
                        diasTrabajados = estadisticas.Count(e => e.asistencia),
                        totalMinutosTrabajados = estadisticas.Sum(e => e.minutos_trabajados ?? 0),
                        totalMinutosRetraso = estadisticas.Sum(e => e.minutos_retraso ?? 0),
                        totalMinutosExtra = estadisticas.Sum(e => e.minutos_extra ?? 0),
                        porcentajeAsistencia = estadisticas.Count > 0 ?
                            Math.Round((double)estadisticas.Count(e => e.asistencia) / estadisticas.Count * 100, 1) : 0,
                        totalHorasTrabajadas = Math.Round(estadisticas.Sum(e => e.minutos_trabajados ?? 0) / 60.0, 1)
                    },
                    metricasAvanzadas = metricas,
                    detallePorDia = estadisticas.Select(e => new
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
                    }),
                    datosGraficos = PrepararDatosParaGraficos(estadisticas, fichajes, fechaInicio, fechaFin)
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener estadísticas detalladas del empleado");
                return StatusCode(500, new { mensaje = "Error al obtener estadísticas" });
            }
        }

        private object CalcularMetricasAvanzadas(List<Estadistica> estadisticas, List<Fichaje> fichajes)
        {
            var diasConDatos = estadisticas.Where(e => e.asistencia).ToList();

            if (diasConDatos.Count == 0)
            {
                return new
                {
                    jornadaPromedio = "0h 0m",
                    eficiencia = 0,
                    mejorDia = "0h 0m",
                    consistencia = 0,
                    tendencia = "estable"
                };
            }

            // Jornada promedio
            var promedioMinutos = (int)diasConDatos.Average(e => e.minutos_trabajados ?? 0);
            var horasPromedio = promedioMinutos / 60;
            var minutosPromedio = promedioMinutos % 60;

            // Eficiencia (basada en asistencia y puntualidad)
            var diasPerfectos = diasConDatos.Count(e =>
                e.minutos_retraso <= 5 && e.estado_dia == "NORMAL");
            var eficiencia = (double)diasPerfectos / diasConDatos.Count * 100;

            // Mejor día
            var mejorDiaMinutos = diasConDatos.Max(e => e.minutos_trabajados ?? 0);
            var mejorDiaHoras = mejorDiaMinutos / 60;
            var mejorDiaMinutosResto = mejorDiaMinutos % 60;

            // Consistencia (variación en horas trabajadas)
            var variaciones = diasConDatos.Select(e => e.minutos_trabajados ?? 0).ToArray();
            var promedio = variaciones.Average();
            var varianza = variaciones.Average(v => Math.Pow(v - promedio, 2));
            var desviacion = Math.Sqrt(varianza);
            var consistencia = Math.Max(0, 100 - (desviacion / promedio * 100));

            // Tendencia (última semana vs semana anterior)
            var tendencia = "estable";
            if (diasConDatos.Count >= 14)
            {
                var ultimaSemana = diasConDatos.TakeLast(7).Sum(e => e.minutos_trabajados ?? 0);
                var semanaAnterior = diasConDatos.Take(7).Sum(e => e.minutos_trabajados ?? 0);
                tendencia = ultimaSemana > semanaAnterior ? "mejorando" :
                           ultimaSemana < semanaAnterior ? "disminuyendo" : "estable";
            }

            return new
            {
                jornadaPromedio = $"{horasPromedio}h {minutosPromedio}m",
                eficiencia = Math.Round(eficiencia, 1),
                mejorDia = $"{mejorDiaHoras}h {mejorDiaMinutosResto}m",
                consistencia = Math.Round(consistencia, 1),
                tendencia = tendencia
            };
        }

        private object PrepararDatosParaGraficos(List<Estadistica> estadisticas, List<Fichaje> fichajes, DateOnly inicio, DateOnly fin)
        {
            // Datos para gráfico de tendencias semanales
            var tendenciasSemanales = new List<object>();
            var fechaActual = inicio;

            while (fechaActual <= fin)
            {
                var fechaFinSemana = fechaActual.AddDays(6) > fin ? fin : fechaActual.AddDays(6);
                var estadisticasSemana = estadisticas
                    .Where(e => e.fecha >= fechaActual && e.fecha <= fechaFinSemana)
                    .ToList();

                var horasSemana = estadisticasSemana.Sum(e => e.minutos_trabajados ?? 0) / 60.0;
                var retrasoSemana = estadisticasSemana.Sum(e => e.minutos_retraso ?? 0);
                var extraSemana = estadisticasSemana.Sum(e => e.minutos_extra ?? 0);

                tendenciasSemanales.Add(new
                {
                    semana = $"Sem {fechaActual:dd/MM}",
                    horas = Math.Round(horasSemana, 1),
                    retraso = retrasoSemana,
                    extra = extraSemana
                });

                fechaActual = fechaActual.AddDays(7);
            }

            // Datos para gráfico de distribución de estados
            var estados = estadisticas
                .Where(e => e.asistencia)
                .GroupBy(e => e.estado_dia)
                .Select(g => new
                {
                    estado = g.Key,
                    cantidad = g.Count(),
                    porcentaje = Math.Round((double)g.Count() / estadisticas.Count(e => e.asistencia) * 100, 1)
                })
                .ToList();

            // Datos para gráfico de progreso diario
            var progresoDiario = estadisticas
                .OrderBy(e => e.fecha)
                .Select(e => new
                {
                    fecha = e.fecha.ToString("dd/MM"),
                    horas = e.minutos_trabajados.HasValue ? Math.Round(e.minutos_trabajados.Value / 60.0, 1) : 0,
                    retraso = e.minutos_retraso ?? 0,
                    extra = e.minutos_extra ?? 0,
                    estado = e.estado_dia
                })
                .ToList();

            return new
            {
                tendenciasSemanales,
                distribucionEstados = estados,
                progresoDiario
            };
        }

        // Agregar en EstadisticasController.cs
        [HttpGet("mis-graficos")]
        public async Task<IActionResult> GetDatosGraficos([FromQuery] string inicio, [FromQuery] string fin)
        {
            try
            {
                var empleadoId = User.FindFirst("EmpleadoId")?.Value;
                if (string.IsNullOrEmpty(empleadoId))
                    return Unauthorized(new { mensaje = "No se pudo identificar al empleado" });

                var fechaInicio = DateOnly.Parse(inicio);
                var fechaFin = DateOnly.Parse(fin);

                // Procesar estadísticas para el rango
                for (var fecha = fechaInicio; fecha <= fechaFin; fecha = fecha.AddDays(1))
                {
                    await _estadisticasService.ProcesarEstadisticasDelDia(fecha);
                }

                var estadisticas = await _context.Estadisticas
                    .Where(e => e.idEmpleadoEstadistica == int.Parse(empleadoId) &&
                               e.fecha >= fechaInicio && e.fecha <= fechaFin)
                    .OrderBy(e => e.fecha)
                    .ToListAsync();

                var fichajes = await _context.Fichajes
                    .Where(f => f.idEmpleadoFichaje == int.Parse(empleadoId) &&
                               f.fecha >= fechaInicio && f.fecha <= fechaFin)
                    .OrderBy(f => f.fecha)
                    .ThenBy(f => f.hora)
                    .ToListAsync();

                var datosGraficos = PrepararDatosParaGraficos(estadisticas, fichajes, fechaInicio, fechaFin);

                return Ok(new
                {
                    periodo = $"{fechaInicio:dd/MM/yyyy} - {fechaFin:dd/MM/yyyy}",
                    datosGraficos
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener datos para gráficos");
                return StatusCode(500, new { mensaje = "Error al obtener datos de gráficos" });
            }
        }





        // AGREGAR EN EstadisticasController.cs
        [HttpGet("resumen-empleado")]
        public async Task<IActionResult> GetResumenEmpleado()
        {
            try
            {
                var empleadoId = User.FindFirst("EmpleadoId")?.Value;
                if (string.IsNullOrEmpty(empleadoId))
                    return Unauthorized(new { mensaje = "No se pudo identificar al empleado" });

                var hoy = DateOnly.FromDateTime(DateTime.Today);
                var inicioMes = new DateOnly(hoy.Year, hoy.Month, 1);

                // Datos del empleado
                var empleado = await _context.Empleados
                    .Include(e => e.Departamento)
                    .FirstOrDefaultAsync(e => e.idEmpleado == int.Parse(empleadoId));

                if (empleado == null)
                    return NotFound(new { mensaje = "Empleado no encontrado" });

                // Fichajes de hoy
                var fichajesHoy = await _context.Fichajes
                    .Where(f => f.idEmpleadoFichaje == int.Parse(empleadoId) && f.fecha == hoy)
                    .OrderByDescending(f => f.hora)
                    .Select(f => new
                    {
                        f.idFichaje,
                        f.tipo,
                        hora = f.hora.ToString("HH:mm"),
                        f.fecha
                    })
                    .ToListAsync();

                // Horario activo
                var horarioActivo = await _context.Horarios
                    .Include(h => h.Turno)
                    .Where(h => h.idHorario_De_Empleado == int.Parse(empleadoId) && h.activo &&
                               h.fecha_inicio <= hoy && (h.fecha_fin == null || h.fecha_fin >= hoy))
                    .Select(h => new
                    {
                        h.Turno.nombre,
                        h.Turno.hora_entrada,
                        h.Turno.hora_salida,
                        h.Turno.tolerancia_minutos
                    })
                    .FirstOrDefaultAsync();

                // Estadísticas del mes
                var estadisticasMes = await _context.Estadisticas
                    .Where(e => e.idEmpleadoEstadistica == int.Parse(empleadoId) &&
                               e.fecha >= inicioMes && e.fecha <= hoy)
                    .ToListAsync();

                var resumenMes = new
                {
                    totalDias = estadisticasMes.Count,
                    diasTrabajados = estadisticasMes.Count(e => e.asistencia),
                    totalHoras = estadisticasMes.Sum(e => e.minutos_trabajados ?? 0) / 60,
                    totalRetraso = estadisticasMes.Sum(e => e.minutos_retraso ?? 0),
                    totalExtra = estadisticasMes.Sum(e => e.minutos_extra ?? 0),
                    porcentajeAsistencia = estadisticasMes.Count > 0 ?
                        Math.Round((double)estadisticasMes.Count(e => e.asistencia) / estadisticasMes.Count * 100, 1) : 0
                };

                return Ok(new
                {
                    empleado = new
                    {
                        id = empleado.idEmpleado,
                        nombre = empleado.nombre,
                        codigo_empleado = empleado.codigo_empleado,
                        foto_url = empleado.foto_url,
                        departamento = empleado.Departamento.nombre
                    },
                    fichajesHoy,
                    horarioActivo,
                    resumenMes
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener resumen del empleado");
                return StatusCode(500, new { mensaje = "Error al obtener resumen" });
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