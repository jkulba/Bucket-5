using System;
using System.Linq;
using Kulba.Service.Bucket.Services;
using Serilog.Core;
using Serilog.Events;

namespace Kulba.Service.Bucket.Extensions
{
    public class BucketSink : ILogEventSink
    {

        public void Emit(LogEvent logEvent)
        {
            if (logEvent == null)
            {
                throw new ArgumentNullException("logEvent");
            }

            foreach(var p in logEvent.Properties)
            {
                // Console.WriteLine("Key: " + p.Key);
                if (p.Key == "ConnectionId")
                {
                    RequestLogEventList.Instance().Add(logEvent);
                }
            }
            
            
        }
    }
}