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
    public class FichajesController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly ILogger<FichajesController> _logger;
        private readonly IRfidCaptureService _captureService;
        public FichajesController(MyDbContext context, ILogger<FichajesController> logger, IRfidCaptureService captureService)
        {
            _context = context;
            _logger = logger;
            _captureService = captureService;
        }

        // POST: api/fichajes/rfid — Para el ESP32
        // POST: api/fichajes/rfid
        [HttpPost("rfid")]
        [AllowAnonymous]
        public async Task<IActionResult> RegistrarFichajeRFID([FromBody] FichajeRFIDRequest request)
        {
            try
            {
                // Buscar tarjeta RFID
                var rfid = await _context.Rfids
                    .Include(r => r.Empleado)
                    .FirstOrDefaultAsync(r => r.codigo_rfid == request.CodigoRFID && r.activo);

                if (rfid == null)
                {
                    // ✅ NUEVO: Notificar al servicio de captura de tarjetas desconocidas
                    _captureService?.CaptureUnknownRfid(request.CodigoRFID);

                    // Devolver error para que el ESP32 sepa que no es válida
                    return BadRequest(new { mensaje = "Tarjeta no válida" });
                }

                var empleado = rfid.Empleado;
                if (!empleado.activo)
                {
                    return BadRequest(new { mensaje = "Empleado inactivo" });
                }

                var hoy = DateOnly.FromDateTime(DateTime.Today); // ✅ Usar Today, no Now
                var ahora = TimeOnly.FromDateTime(DateTime.Now);

                // Determinar tipo de fichaje
                var ultimoFichaje = await _context.Fichajes
                    .Where(f => f.idEmpleadoFichaje == empleado.idEmpleado && f.fecha == hoy)
                    .OrderByDescending(f => f.hora)
                    .FirstOrDefaultAsync();

                string tipoFichaje = "Entrada";
                if (ultimoFichaje != null)
                {
                    tipoFichaje = ultimoFichaje.tipo == "Entrada" ? "Salida" : "Entrada";
                }

                // Crear fichaje
                var fichaje = new Fichaje
                {
                    idEmpleadoFichaje = empleado.idEmpleado,
                    fecha = hoy,
                    hora = ahora,
                    tipo = tipoFichaje,
                    ip = request.IP,
                    observacion = "Registro por RFID"
                };

                _context.Fichajes.Add(fichaje);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    mensaje = $"Fichaje de {tipoFichaje} registrado",
                    empleado = empleado.nombre,
                    hora = ahora.ToString("HH:mm"),
                    tipo = tipoFichaje
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al registrar fichaje RFID");
                return StatusCode(500, new { mensaje = "Error interno" });
            }
        }

        // GET: api/fichajes/hoy
        [HttpGet("hoy")]
        [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> GetFichajesHoy()
        {
            try
            {
                var hoy = DateOnly.FromDateTime(DateTime.Today);
                var fichajes = await _context.Fichajes
                    .Include(f => f.Empleado)
                    .Where(f => f.fecha == hoy)
                    .OrderByDescending(f => f.hora)
                    .Select(f => new
                    {
                        f.idFichaje,
                        empleado = f.Empleado.nombre,
                        f.tipo,
                        hora = f.hora.ToString("HH:mm"),
                        f.ip
                    })
                    .ToListAsync();

                return Ok(fichajes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener fichajes de hoy");
                return StatusCode(500, new { mensaje = "Error al obtener fichajes de hoy" });
            }
        }

        // GET: api/fichajes/empleado/{id}?fecha=...
        [HttpGet("empleado/{idEmpleado}")]
        [Authorize]
        public async Task<IActionResult> GetFichajesPorEmpleado(int idEmpleado, [FromQuery] string fecha = null)
        {
            try
            {
                var query = _context.Fichajes.Where(f => f.idEmpleadoFichaje == idEmpleado);
                if (!string.IsNullOrEmpty(fecha) && DateOnly.TryParse(fecha, out var fechaFiltro))
                    query = query.Where(f => f.fecha == fechaFiltro);

                var fichajes = await query
                    .OrderBy(f => f.fecha)
                    .ThenBy(f => f.hora)
                    .Select(f => new
                    {
                        f.idFichaje,
                        f.fecha,
                        hora = f.hora.ToString("HH:mm"),
                        f.tipo,
                        f.observacion
                    })
                    .ToListAsync();

                return Ok(fichajes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener fichajes por empleado");
                return StatusCode(500, new { mensaje = "Error al obtener fichajes" });
            }
        }

        // ✅ NUEVO ENDPOINT: GET /api/fichajes?fecha=2025-11-02&empleadoId=5&tipo=Entrada
        [HttpGet]
        [Authorize(Roles = "Admin,Supervisor")]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? fecha = null,
            [FromQuery] int? empleadoId = null,
            [FromQuery] string? tipo = null)
        {
            try
            {
                var query = _context.Fichajes
                    .Include(f => f.Empleado)
                    .AsQueryable();

                // Filtro por fecha
                if (!string.IsNullOrEmpty(fecha) && DateOnly.TryParse(fecha, out var fechaFiltro))
                    query = query.Where(f => f.fecha == fechaFiltro);

                if (empleadoId.HasValue)
                    query = query.Where(f => f.idEmpleadoFichaje == empleadoId.Value);

                if (!string.IsNullOrEmpty(tipo))
                    query = query.Where(f => f.tipo == tipo);

                var fichajes = await query
                    .OrderByDescending(f => f.fecha)
                    .ThenByDescending(f => f.hora)
                    .Select(f => new
                    {
                        f.idFichaje,
                        f.fecha,
                        hora = f.hora.ToString("HH:mm"),
                        f.tipo,
                        f.ip,
                        f.observacion,
                        empleado = f.Empleado.nombre
                    })
                    .ToListAsync();

                return Ok(fichajes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener fichajes con filtros");
                return StatusCode(500, new { mensaje = "Error al obtener fichajes" });
            }
        }
    }

    public class FichajeRFIDRequest
    {
        public string CodigoRFID { get; set; }
        public string IP { get; set; }
    }
}