// Services/TelegramService.cs
using fiexpress.Data;
using fiexpress.Models;
using Microsoft.Extensions.Options;
using fiexpress.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json;

namespace fiexpress.Services
{
    public interface ITelegramService
    {
        Task<bool> SendRFIDAlertAsync(string codigoRFID, string ip);
        Task<bool> SendToAdminsAsync(string message);
        Task<bool> SendToSupervisorsAsync(string message);
        Task<bool> SendBroadcastAsync(string message);
        Task<List<long>> GetAdminChatIdsAsync();
    }

    public class TelegramService : ITelegramService
    {
        private readonly TelegramSettings _settings;
        private readonly MyDbContext _context;
        private readonly ILogger<TelegramService> _logger;
        private readonly HttpClient _httpClient;

        public TelegramService(IOptions<TelegramSettings> settings, MyDbContext context,
                             ILogger<TelegramService> logger, HttpClient httpClient)
        {
            _settings = settings.Value;
            _context = context;
            _logger = logger;
            _httpClient = httpClient;
        }

       

        public async Task<bool> SendToAdminsAsync(string message)
        {
            var adminChatIds = await GetAdminChatIdsAsync();
            return await SendBulkMessageAsync(adminChatIds, message);
        }

        public async Task<bool> SendToSupervisorsAsync(string message)
        {
            var supervisorChatIds = await GetSupervisorChatIdsAsync();
            return await SendBulkMessageAsync(supervisorChatIds, message);
        }

        public async Task<bool> SendBroadcastAsync(string message)
        {
            var allChatIds = await GetAllAdminAndSupervisorChatIdsAsync();
            return await SendBulkMessageAsync(allChatIds, message);
        }

        public async Task<List<long>> GetAdminChatIdsAsync()
        {
            return await GetAllAdminAndSupervisorChatIdsAsync();
        }

        public async Task<bool> SendRFIDAlertAsync(string codigoRFID, string ip)
        {
            try
            {
                _logger.LogInformation($"🔔 INICIANDO ALERTA TELEGRAM - RFID: {codigoRFID}, IP: {ip}");

                var message = $"🚨 *ALERTA DE SEGURIDAD* 🚨\n\n" +
                             $"*Intento de fichaje no autorizado detectado*\n\n" +
                             $"📋 *RFID:* `{codigoRFID}`\n" +
                             $"🌐 *IP:* `{ip}`\n" +
                             $"📍 *Origen:* `{(ip.Contains("ESP32") ? "Dispositivo Físico" : "Swagger/Web")}`\n" +
                             $"⏰ *Hora:* `{DateTime.Now:dd/MM/yyyy HH:mm:ss}`\n\n" +
                             $"⚠️ *Revisar sistema inmediatamente*";

                _logger.LogInformation($"📝 Mensaje Telegram preparado: {message}");

                var result = await SendToAdminsAndSupervisorsAsync(message);

                _logger.LogInformation($"📤 Resultado envío Telegram: {result}");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ ERROR CRÍTICO en SendRFIDAlertAsync");
                return false;
            }
        }

        private async Task<bool> SendToAdminsAndSupervisorsAsync(string message)
        {
            try
            {
                _logger.LogInformation("🔍 Buscando Chat IDs para notificación...");

                var chatIds = await GetAllAdminAndSupervisorChatIdsAsync();

                _logger.LogInformation($"👥 Chat IDs encontrados: {chatIds.Count} -> [{string.Join(", ", chatIds)}]");

                if (!chatIds.Any())
                {
                    _logger.LogWarning("⚠️ No hay Chat IDs configurados para enviar notificaciones");
                    return false;
                }

                var result = await SendBulkMessageAsync(chatIds, message);
                _logger.LogInformation($"📊 Resultado envío masivo: {result} (para {chatIds.Count} chats)");

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Error en SendToAdminsAndSupervisorsAsync");
                return false;
            }
        }

        private async Task<List<long>> GetAllAdminAndSupervisorChatIdsAsync()
        {
            try
            {
                // Primero intentar obtener de la base de datos
                var chatIdsFromDb = await _context.Usuarios
                    .Include(u => u.Empleado)
                    .Where(u => u.activo &&
                           (u.Empleado.Rol.nombre == "Admin" || u.Empleado.Supervisor != null) &&
                           u.Empleado.telegram_chat_id != null)
                    .Select(u => u.Empleado.telegram_chat_id.Value)
                    .ToListAsync();

                if (chatIdsFromDb.Any())
                {
                    _logger.LogInformation($"📊 Chat IDs desde BD: {string.Join(", ", chatIdsFromDb)}");
                    return chatIdsFromDb;
                }

                // Si no hay en BD, usar los de configuración
                if (_settings.AdminChatIds.Any())
                {
                    _logger.LogInformation($"📊 Chat IDs desde configuración: {string.Join(", ", _settings.AdminChatIds)}");
                    return _settings.AdminChatIds.ToList();
                }

                _logger.LogWarning("⚠️ No se encontraron Chat IDs configurados");
                return new List<long>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Error obteniendo Chat IDs");
                return _settings.AdminChatIds.Any() ? _settings.AdminChatIds.ToList() : new List<long>();
            }
        }

        private async Task<List<long>> GetSupervisorChatIdsAsync()
        {
            var chatIds = await _context.Usuarios
                .Include(u => u.Empleado)
                .Where(u => u.activo && u.Empleado.Supervisor != null &&
                       u.Empleado.telegram_chat_id != null)
                .Select(u => u.Empleado.telegram_chat_id.Value)
                .ToListAsync();

            return chatIds;
        }

        private async Task<bool> SendBulkMessageAsync(List<long> chatIds, string message)
        {
            if (!chatIds.Any())
            {
                _logger.LogWarning("⚠️ No hay Chat IDs para enviar mensajes");
                return false;
            }

            var tasks = chatIds.Select(chatId => SendSingleMessageAsync(chatId, message));
            var results = await Task.WhenAll(tasks);

            var successCount = results.Count(r => r);
            _logger.LogInformation($"📤 Telegram: {successCount}/{chatIds.Count} mensajes enviados exitosamente");

            return successCount > 0;
        }


        private async Task<bool> SendSingleMessageAsync(long chatId, string message)
        {
            try
            {
                _logger.LogInformation($"📤 Enviando Telegram a ChatID: {chatId}");

                var url = $"https://api.telegram.org/bot{_settings.BotToken}/sendMessage";

                var payload = new
                {
                    chat_id = chatId,
                    text = message,
                    parse_mode = "Markdown",
                    disable_notification = false
                };

                var json = JsonSerializer.Serialize(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // ✅ Agregar timeout y políticas de retry
                using var timeoutToken = new CancellationTokenSource(TimeSpan.FromSeconds(15));

                var response = await _httpClient.PostAsync(url, content, timeoutToken.Token);

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation($"✅ Telegram enviado EXITOSAMENTE a ChatID: {chatId}");
                    return true;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"❌ Error Telegram para {chatId}: {response.StatusCode} - {errorContent}");
                    return false;
                }
            }
            catch (TaskCanceledException)
            {
                _logger.LogError($"⏰ TIMEOUT enviando Telegram a {chatId}");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"💥 ERROR CRÍTICO enviando Telegram a {chatId}");
                return false;
            }
        }

    }
}