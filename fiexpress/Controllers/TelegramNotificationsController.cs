using fiexpress.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace fiexpress.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous] // El ESP32 necesita acceso sin autenticación
    public class TelegramNotificationsController : ControllerBase
    {
        private readonly ITelegramService _telegramService;
        private readonly ILogger<TelegramNotificationsController> _logger;

        public TelegramNotificationsController(ITelegramService telegramService, ILogger<TelegramNotificationsController> logger)
        {
            _telegramService = telegramService;
            _logger = logger;
        }

        // POST: api/telegramnotifications/fichaje-invalido
        [HttpPost("fichaje-invalido")]
        public async Task<IActionResult> NotificarFichajeInvalido([FromBody] NotificacionFichajeRequest request)
        {
            try
            {
                _logger.LogInformation($"🚨 NOTIFICACIÓN TELEGRAM SOLICITADA DIRECTAMENTE");
                _logger.LogInformation($"📋 RFID: {request.CodigoRFID}, IP: {request.IP}, Tipo: {request.Tipo}");

                string mensaje = "";

                switch (request.Tipo.ToUpper())
                {
                    case "INVALIDO":
                        mensaje = $" *ALERTA DE SEGURIDAD - FICHAJE INVÁLIDO* \n\n" +
                                 $"*Intento de fichaje no autorizado detectado*\n\n" +
                                 $" *RFID:* `{request.CodigoRFID}`\n" +
                                 $" *IP Dispositivo:* `{request.IP}`\n" +
                                 $" *Origen:* `ESP32 Físico`\n" +
                                 $" *Hora:* `{DateTime.Now:dd/MM/yyyy HH:mm:ss}`\n\n" +
                                 $" *Revisar sistema inmediatamente*";
                        break;

                    case "VALIDO":
                        mensaje = $" *FICHAJE REGISTRADO EXITOSAMENTE* \n\n" +
                                 $"*Fichaje válido procesado*\n\n" +
                                 $" *RFID:* `{request.CodigoRFID}`\n" +
                                 $" *Empleado:* `{request.NombreEmpleado ?? "No especificado"}`\n" +
                                 $" *IP Dispositivo:* `{request.IP}`\n" +
                                 $" *Origen:* `ESP32 Físico`\n" +
                                 $" *Hora:* `{DateTime.Now:dd/MM/yyyy HH:mm:ss}`\n\n" +
                                 $" *Fichaje procesado correctamente*";
                        break;

                    case "ERROR":
                        mensaje = $" *ERROR EN FICHAJE* \n\n" +
                                 $"*Error al procesar fichaje*\n\n" +
                                 $" *RFID:* `{request.CodigoRFID}`\n" +
                                 $" *IP Dispositivo:* `{request.IP}`\n" +
                                 $" *Origen:* `ESP32 Físico`\n" +
                                 $" *Hora:* `{DateTime.Now:dd/MM/yyyy HH:mm:ss}`\n\n" +
                                 $" *Revisar conexión con el servidor*";
                        break;

                    default:
                        return BadRequest(new { mensaje = "Tipo de notificación no válido" });
                }

                var resultado = await _telegramService.SendToAdminsAsync(mensaje);

                _logger.LogInformation($"📤 Notificación Telegram enviada: {resultado}");

                return Ok(new
                {
                    mensaje = "Notificación enviada",
                    enviado = resultado,
                    timestamp = DateTime.Now
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "💥 Error en notificación Telegram directa");
                return StatusCode(500, new { mensaje = "Error al enviar notificación", error = ex.Message });
            }
        }

        // POST: api/telegramnotifications/test
        [HttpPost("test")]
        public async Task<IActionResult> TestNotificacion([FromBody] TestNotificacionRequest request)
        {
            try
            {
                _logger.LogInformation($" TEST NOTIFICACIÓN DIRECTA: {request.Mensaje}");

                var mensaje = $" *TEST NOTIFICACIÓN DIRECTA* \n\n" +
                             $"*Mensaje de prueba desde ESP32*\n\n" +
                             $" *Test:* `{request.Mensaje}`\n" +
                             $" *IP:* `{request.IP ?? "No especificada"}`\n" +
                             $" *Hora:* `{DateTime.Now:dd/MM/yyyy HH:mm:ss}`\n\n" +
                             $" *Conexión Telegram funcionando correctamente*";

                var resultado = await _telegramService.SendToAdminsAsync(mensaje);

                return Ok(new
                {
                    mensaje = "Test de notificación completado",
                    enviado = resultado,
                    timestamp = DateTime.Now
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "💥 Error en test de notificación");
                return StatusCode(500, new { mensaje = "Error en test", error = ex.Message });
            }
        }
    }

    // DTOs para las notificaciones
    public class NotificacionFichajeRequest
    {
        public string CodigoRFID { get; set; }
        public string IP { get; set; }
        public string Tipo { get; set; } // "INVALIDO", "VALIDO", "ERROR"
        public string NombreEmpleado { get; set; } = string.Empty;
    }

    public class TestNotificacionRequest
    {
        public string Mensaje { get; set; }
        public string IP { get; set; }
    }
}