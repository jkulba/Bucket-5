using System;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using Serilog.Core;
using Serilog.Events;

namespace Kulba.Service.Bucket.Sinks
{
    public class RequestLogEventSink : ILogEventSink
    {
        private readonly IMemoryCache _memoryCache;
        
        public RequestLogEventSink(IMemoryCache memoryCache)
        {
            this._memoryCache = memoryCache;
        }

        public void Emit(LogEvent logEvent)
        {
            Console.WriteLine("YUCK");
            this._memoryCache.Set("LogEventCache", logEvent.MessageTemplate.Text);
        }
    }
}