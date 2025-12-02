using System.Collections.Concurrent;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace fiexpress.Services
{
    public interface IRfidCaptureService
    {
        void StartCapture(int supervisorId, int durationSeconds = 30);
        string GetLastCapturedRfid(int supervisorId);
        bool IsCaptureActive(int supervisorId);
        void CaptureUnknownRfid(string rfidCode);
        void RegisterHttpContextAccessor(IHttpContextAccessor httpContextAccessor);
    }

    public class RfidCaptureService : IRfidCaptureService
    {
        private readonly ConcurrentDictionary<int, (string Rfid, DateTime ExpiresAt)> _captures = new();
        private readonly ConcurrentBag<string> _unknownRfids = new();
        private IHttpContextAccessor _httpContextAccessor;

        public void RegisterHttpContextAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void StartCapture(int supervisorId, int durationSeconds = 30)
        {
            _captures[supervisorId] = (null, DateTime.Now.AddSeconds(durationSeconds));
        }

        public string GetLastCapturedRfid(int supervisorId)
        {
            if (_captures.TryGetValue(supervisorId, out var capture) && capture.Rfid != null)
            {
                _captures.TryRemove(supervisorId, out _);
                return capture.Rfid;
            }
            return null;
        }

        public bool IsCaptureActive(int supervisorId)
        {
            if (_captures.TryGetValue(supervisorId, out var capture))
            {
                if (DateTime.Now > capture.ExpiresAt)
                {
                    _captures.TryRemove(supervisorId, out _);
                    return false;
                }
                return true;
            }
            return false;
        }

        public void CaptureUnknownRfid(string rfidCode)
        {
            // Notificar a todos los supervisores que están en modo captura
            var activeSupervisors = new List<int>();
            foreach (var kvp in _captures)
            {
                if (DateTime.Now <= kvp.Value.ExpiresAt)
                {
                    activeSupervisors.Add(kvp.Key);
                }
            }

            foreach (var supId in activeSupervisors)
            {
                _captures[supId] = (rfidCode, _captures[supId].ExpiresAt);
            }
        }
    }
}