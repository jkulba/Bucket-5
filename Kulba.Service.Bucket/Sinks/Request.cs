using System;
using System.Collections.Generic;
using System.Threading;
using Serilog.Core;
using Serilog.Events;

namespace Kulba.Service.Bucket.Sinks
{
    public class Request : ILogEventSink, IDisposable
    {
        private readonly List<LogEvent> _logEvents;

        public Request()
        {
            _logEvents = new List<LogEvent>();
        }

        public void Dispose()
        {
            _logEvents.Clear();
        }

        public void Emit(LogEvent logEvent)
        {
            Console.WriteLine("logEvent Size: " + _logEvents.Count);
            _logEvents.Add(logEvent);

        }
    }
}