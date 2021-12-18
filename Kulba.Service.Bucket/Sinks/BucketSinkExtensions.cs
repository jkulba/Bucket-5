using Serilog;
using Serilog.Configuration;
using System;

namespace Kulba.Service.Bucket.Sinks
{
    public static class BucketSinkExtensions
    {
        public static LoggerConfiguration Request(this LoggerSinkConfiguration loggerSinkConfiguration, Serilog.Events.LogEventLevel restrictedToMinimumLevel)
        {
            return loggerSinkConfiguration.Sink(new Request(), restrictedToMinimumLevel);
        }

    }
}