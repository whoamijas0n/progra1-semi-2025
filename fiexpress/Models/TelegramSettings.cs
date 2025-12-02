// Models/TelegramSettings.cs
namespace fiexpress.Models
{
    public class TelegramSettings
    {
        public string BotToken { get; set; } = string.Empty;
        public long[] AdminChatIds { get; set; } = Array.Empty<long>();
    }
}