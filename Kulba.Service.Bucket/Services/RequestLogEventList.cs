using System;
using System.Collections.Generic;
using Serilog.Events;

namespace Kulba.Service.Bucket.Services
{
    public class RequestLogEventList : IRequestLogEventList
    {
        private static RequestLogEventList _instance = null;
        private readonly List<LogEvent> _logEvents = new();
        private static object lockThis = new object();

        private RequestLogEventList() {}

        public static RequestLogEventList Instance()
        {
            lock(lockThis)
            {
                if (RequestLogEventList._instance == null)
                {
                    _instance = new RequestLogEventList();
                }
            }
            return _instance;
        } 

        public void Add(LogEvent logEvent)
        {
            _logEvents.Add(logEvent);
             Console.WriteLine("LogEventListSize: " + _logEvents.Count);
        }

        public List<LogEvent> Fetch(string id)
        {
            List<LogEvent> results = new();

            


            return results;
        }

        public void Flush(string id)
        {
            _logEvents.Clear();
            Console.WriteLine("LogEventListSize: " + _logEvents.Count);
        }
    }
}