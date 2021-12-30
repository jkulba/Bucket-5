using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Serilog.Events;

namespace Kulba.Service.Bucket.Services
{
    public class RequestLogEventList : IRequestLogEventList
    {
        private readonly List<LogEvent> _logEvents = new();
        private readonly ILogger<RequestLogEventList> _logger;

        public RequestLogEventList(ILogger<RequestLogEventList> logger)
        {
            _logger = logger;
        }
        public void Add(LogEvent logEvent)
        {
            _logEvents.Add(logEvent);
        }

        public List<LogEvent> Fetch(string id)
        {
            return _logEvents;
        }

        public void Flush(string id)
        {
            _logEvents.Clear();
        }
    }
}