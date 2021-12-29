using System.Collections.Generic;
using Serilog.Events;

namespace Kulba.Service.Bucket.Services
{
    public interface IRequestLogEventList
    {
        public void Add(LogEvent logEvent);

        public List<LogEvent> Fetch(string id);

        public void Flush(string id);
    }
}