using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Serilog.Events;

namespace Kulba.Service.Bucket.Services
{
    public sealed class RequestLogEventList : IRequestLogEventList
    {
        private readonly List<LogEvent> _logEvents = new();
        static RequestLogEventList() {}
        private RequestLogEventList() {}

        public static IRequestLogEventList Instance { get; } = new RequestLogEventList();
        public void Add(LogEvent logEvent)
        {
            _logEvents.Add(logEvent);
        }

        public List<LogEvent> Fetch(string id)
        {
            throw new System.NotImplementedException();
        }

        public void Flush(string id)
        {
            _logEvents.Clear();
        }
    }
}