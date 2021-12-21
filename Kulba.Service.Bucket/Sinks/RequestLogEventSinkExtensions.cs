using Microsoft.Extensions.Caching.Memory;
using Serilog;
using Serilog.Configuration;
using System;

namespace Kulba.Service.Bucket.Sinks
{
    public static class RequestLogEventSinkExtensions
    {
        public static LoggerConfiguration RequestLogEventSink(this LoggerSinkConfiguration loggerSinkConfiguration, IMemoryCache memoryCache, Serilog.Events.LogEventLevel restrictedToMinimumLevel)
        {
            return loggerSinkConfiguration.Sink(new RequestLogEventSink(memoryCache), restrictedToMinimumLevel);
        }

    }
}