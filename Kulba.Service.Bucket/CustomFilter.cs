using Serilog.Core;
using Serilog.Events;

namespace Kulba.Service.Bucket
{
        public class CustomFilter : ILogEventFilter
    {
        readonly LogEventLevel _levelFilter;

        public CustomFilter(LogEventLevel levelFilter = LogEventLevel.Information)
        {
            _levelFilter = levelFilter;
        }

        public bool IsEnabled(LogEvent logEvent)
        {
            return logEvent.Level >= _levelFilter;
        }
    }
}