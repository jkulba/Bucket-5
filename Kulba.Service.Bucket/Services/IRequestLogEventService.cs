using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Serilog.Core;
using Serilog.Events;

namespace Kulba.Service.Bucket.Services
{
    public interface IRequestLogEventService
    {
        Task<List<LogEvent>> RequestLogEvents();
        Task AddLogEvent(LogEvent logEvent);

    }    
}