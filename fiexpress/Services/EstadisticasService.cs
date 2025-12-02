using fiexpress.Data;
using fiexpress.Extensions;
using fiexpress.Models;
using Microsoft.EntityFrameworkCore;

namespace fiexpress.Services
{
    public interface IEstadisticasService
    {
        Task ProcesarEstadisticasDelDia(DateOnly fecha);
        Task<Estadistica> CalcularEstadisticaEmpleado(int empleadoId, DateOnly fecha);
        Task<List<Estadistica>> ObtenerEstadisticasGenerales(DateOnly inicio, DateOnly fin);
    }

    public class EstadisticasService : IEstadisticasService
    {
        private readonly MyDbContext _context;
        private readonly ILogger<EstadisticasService> _logger;

        public EstadisticasService(MyDbContext context, ILogger<EstadisticasService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task ProcesarEstadisticasDelDia(DateOnly fecha)
        {
            try
            {
                var empleadosActivos = await _context.Empleados
                    .Where(e => e.activo)
                    .ToListAsync();

                foreach (var empleado in empleadosActivos)
                {
                    var estadistica = await CalcularEstadisticaEmpleado(empleado.idEmpleado, fecha);

                    // Guardar o actualizar estadística
                    var existente = await _context.Estadisticas
                        .FirstOrDefaultAsync(e => e.idEmpleadoEstadistica == empleado.idEmpleado && e.fecha == fecha);

                    if (existente != null)
                    {
                        existente.minutos_trabajados = estadistica.minutos_trabajados;
                        existente.minutos_retraso = estadistica.minutos_retraso;
                        existente.minutos_extra = estadistica.minutos_extra;
                        existente.asistencia = estadistica.asistencia;
                        existente.estado_dia = estadistica.estado_dia;
                    }
                    else
                    {
                        _context.Estadisticas.Add(estadistica);
                    }
                }

                await _context.SaveChangesAsync();
                _logger.LogInformation($"Estadísticas procesadas para {fecha}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al procesar estadísticas del día");
                throw;
            }
        }
        public async Task<Estadistica> CalcularEstadisticaEmpleado(int empleadoId, DateOnly fecha)
        {
            try
            {
                // 🔸 BUSCAR EL HORARIO QUE CUBRE ESA FECHA EXACTA (ACTIVO O NO)
                // ✅ BUSCAR EL HORARIO QUE ERA VÁLIDO EN ESA FECHA, SIN IMPORTAR SI HOY ESTÁ ACTIVO
                var horario = await _context.Horarios
                    .Include(h => h.Turno)
                    .Where(h => h.idHorario_De_Empleado == empleadoId &&
                               h.fecha_inicio <= fecha &&
                               (h.fecha_fin == null || h.fecha_fin >= fecha))
                    .OrderByDescending(h => h.fecha_inicio) // Por si hay solapamiento, el más reciente del período
                    .FirstOrDefaultAsync();

                // Obtener fichajes del día
                var fichajes = await _context.Fichajes
                    .Where(f => f.idEmpleadoFichaje == empleadoId && f.fecha == fecha)
                    .OrderBy(f => f.hora)
                    .ToListAsync();

                var estadistica = new Estadistica
                {
                    idEmpleadoEstadistica = empleadoId,
                    fecha = fecha,
                    asistencia = false,
                    estado_dia = "AUSENTE",
                    minutos_trabajados = 0,
                    minutos_retraso = 0,
                    minutos_extra = 0
                };

                // Si no tiene horario asignado ese día → no laboral
                if (horario == null || !EsDiaLaboral(horario.Turno, fecha))
                {
                    estadistica.estado_dia = "NO_LABORAL";
                    return estadistica;
                }

                var entradaFichaje = fichajes.FirstOrDefault(f => f.tipo == "Entrada");
                var salidaFichaje = fichajes.LastOrDefault(f => f.tipo == "Salida");

                // Sin entrada → ausente
                if (entradaFichaje == null)
                {
                    return estadistica;
                }

                estadistica.asistencia = true;
                var horaEntradaReal = entradaFichaje.hora;
                var horaSalidaReal = salidaFichaje?.hora;
                var turno = horario.Turno;

                // 🔸 CALCULAR RETRASO
                var tolerancia = TimeSpan.FromMinutes(turno.tolerancia_minutos);
                var horaEntradaEsperada = turno.hora_entrada.ToTimeSpan();
                var horaEntradaRealTs = horaEntradaReal.ToTimeSpan();

                if (horaEntradaRealTs > horaEntradaEsperada + tolerancia)
                {
                    var retrasoTotal = (int)(horaEntradaRealTs - horaEntradaEsperada).TotalMinutes;
                    estadistica.minutos_retraso = Math.Max(0, retrasoTotal - turno.tolerancia_minutos);
                }

                // 🔸 CALCULAR TIEMPO TRABAJADO Y EXTRA
                if (horaSalidaReal.HasValue)
                {
                    var salidaTs = horaSalidaReal.Value.ToTimeSpan();
                    var horasTrabajadas = (int)(salidaTs - horaEntradaRealTs).TotalMinutes;
                    estadistica.minutos_trabajados = Math.Max(0, horasTrabajadas);

                    var horasEsperadas = (int)(turno.hora_salida.ToTimeSpan() - turno.hora_entrada.ToTimeSpan()).TotalMinutes;

                    if (horasTrabajadas > horasEsperadas)
                    {
                        estadistica.minutos_extra = horasTrabajadas - horasEsperadas;
                    }
                }

                // 🔸 DETERMINAR ESTADO DEL DÍA
                estadistica.estado_dia = DeterminarEstadoDia(estadistica, turno);

                return estadistica;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al calcular estadística para empleado {empleadoId} en {fecha}");
                throw;
            }
        }


        private bool EsDiaLaboral(Turno turno, DateOnly fecha)
        {
            var diaSemana = fecha.DayOfWeek;
            return diaSemana switch
            {
                DayOfWeek.Monday => turno.lunes,
                DayOfWeek.Tuesday => turno.martes,
                DayOfWeek.Wednesday => turno.miercoles,
                DayOfWeek.Thursday => turno.jueves,
                DayOfWeek.Friday => turno.viernes,
                DayOfWeek.Saturday => turno.sabado,
                DayOfWeek.Sunday => turno.domingo,
                _ => false
            };
        }

        private string DeterminarEstadoDia(Estadistica estadistica, Turno turno)
        {
            if (!estadistica.asistencia) return "AUSENTE";
            if (estadistica.minutos_retraso > 30) return "RETRASO_GRAVE";
            if (estadistica.minutos_retraso > 0) return "RETRASO_LEVE";
            if (estadistica.minutos_extra > 60) return "EXTRA_ALTO";
            if (estadistica.minutos_extra > 0) return "EXTRA_NORMAL";
            return "NORMAL";
        }

        public async Task<List<Estadistica>> ObtenerEstadisticasGenerales(DateOnly inicio, DateOnly fin)
        {
            return await _context.Estadisticas
                .Include(e => e.Empleado)
                .Where(e => e.fecha >= inicio && e.fecha <= fin)
                .ToListAsync();
        }
    }
}