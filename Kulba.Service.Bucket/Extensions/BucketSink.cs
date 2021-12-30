using System;
using System.Linq;
using Serilog.Core;
using Serilog.Events;

namespace Kulba.Service.Bucket.Extensions
{
    public class BucketSink : ILogEventSink
    {
        // private readonly ILogEventSink _wrappedSink;

        // public BucketSink(ILogEventSink wrappedSink)
        // {
        //     _wrappedSink = wrappedSink;
        // }

        private static int i = 0;

        public void Emit(LogEvent logEvent)
        {
            var count = i++;
            Console.WriteLine("HIT BUCKET SINK: " +  count);
        }
    }
}