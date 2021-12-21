using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Serilog.Events;

namespace Kulba.Service.Bucket.Services
{
    public class RequestLogEventService : IRequestLogEventService
    {
        private readonly ILogger<RequestLogEventService> _logger;

        private readonly List<LogEvent> _logEvents = new();

        public RequestLogEventService(ILogger<RequestLogEventService> logger)
        {
            _logger = logger;
        }

        public async Task AddLogEvent(LogEvent logEvent)
        {
            _logger.LogInformation("Hit AddLogEvent");

            _logEvents.Add(logEvent);
            _logger.LogDebug("logEvents size: {count}", _logEvents.Count);
            await Task.CompletedTask;    
        }

        public async Task<List<LogEvent>> RequestLogEvents()
        {
            return await Task.FromResult(_logEvents);
        }
    }
}