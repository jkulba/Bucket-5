using System;
using Serilog;
using Serilog.Configuration;
using Serilog.Events;

namespace Kulba.Service.Bucket.Extensions
{
    public static class BucketSinkExtensions
    {
        public static LoggerConfiguration BucketSink(
            this LoggerSinkConfiguration loggerSinkConfiguration)
        {
            return loggerSinkConfiguration.Sink(new BucketSink(), LevelAlias.Minimum, levelSwitch: null);
        }
    }
}