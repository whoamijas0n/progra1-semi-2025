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
        private readonly ITelegramService _telegramService; 

        public FichajesController(MyDbContext context, ILogger<FichajesController> logger,
                                IRfidCaptureService captureService,
                                ITelegramService telegramService) 
        {
            _context = context;
            _logger = logger;
            _captureService = captureService;
            _telegramService = telegramService; 
        }

        [HttpPost("rfid")]
        [AllowAnonymous]
        public async Task<IActionResult> RegistrarFichajeRFID([FromBody] FichajeRFIDRequest request)
        {
            try
            {
                _logger.LogInformation($" RFID recibido: {request.CodigoRFID} desde IP: {request.IP}");

                // DEBUG: Verificar si el servicio Telegram está disponible
                if (_telegramService == null)
                {
                    _logger.LogError("❌ CRÍTICO: _telegramService es NULL");
                    return StatusCode(500, new { mensaje = "Error de configuración del servicio" });
                }
                else
                {
                    _logger.LogInformation("✅ TelegramService está disponible");
                }

                // Buscar tarjeta RFID
                var rfid = await _context.Rfids
                    .Include(r => r.Empleado)
                    .FirstOrDefaultAsync(r => r.codigo_rfid == request.CodigoRFID && r.activo);

                _logger.LogInformation($"🔍 Resultado búsqueda RFID: {(rfid != null ? "ENCONTRADA" : "NO ENCONTRADA")}");

                if (rfid == null)
                {
                    _logger.LogWarning($"❌ Tarjeta no registrada: {request.CodigoRFID}");

                    // ✅ 1. ALERTA INMEDIATA POR TELEGRAM
                    _logger.LogInformation("📤 Intentando enviar alerta por Telegram...");
                    var telegramSent = await _telegramService.SendRFIDAlertAsync(request.CodigoRFID, request.IP);
                    _logger.LogInformation($"📤 Resultado Telegram: {(telegramSent ? "ÉXITO" : "FALLÓ")}");


                    // ✅ 3. Enviar a captura
                    _captureService?.CaptureUnknownRfid(request.CodigoRFID);

                    return BadRequest(new
                    {
                        mensaje = "Tarjeta no válida",
                        telegram_enviado = telegramSent,
                        codigo_rfid = request.CodigoRFID
                    });
                }

                var empleado = rfid.Empleado;
                _logger.LogInformation($"✅ Tarjeta válida para: {empleado.nombre}");

                if (!empleado.activo)
                {
                    _logger.LogWarning($"❌ Empleado inactivo: {empleado.nombre}");
                    return BadRequest(new { mensaje = "Empleado inactivo" });
                }

                // ... resto del código para fichajes válidos
                var hoy = DateOnly.FromDateTime(DateTime.Today);
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
                    _logger.LogInformation($"🔄 Cambiando tipo a: {tipoFichaje} (último: {ultimoFichaje.tipo})");
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

                _logger.LogInformation($"✅ Fichaje de {tipoFichaje} registrado para {empleado.nombre}");

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
                _logger.LogError(ex, "❌ Error al registrar fichaje RFID");
                return StatusCode(500, new { mensaje = "Error interno del servidor" });
            }
        }


        // ✅ ENDPOINT DE PRUEBA PARA TELEGRAM
        [HttpPost("test-telegram")]
        [AllowAnonymous]
        public async Task<IActionResult> TestTelegram([FromBody] TestTelegramRequest request)
        {
            try
            {
                _logger.LogInformation($"🧪 TEST TELEGRAM solicitado: {request.Message}");

                var result = await _telegramService.SendToAdminsAsync($"🧪 TEST: {request.Message}");

                return Ok(new
                {
                    mensaje = "Test de Telegram completado",
                    enviado = result,
                    timestamp = DateTime.Now
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Error en test Telegram");
                return StatusCode(500, new { mensaje = "Error en test", error = ex.Message });
            }
        }

        public class TestTelegramRequest
        {
            public string Message { get; set; }
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