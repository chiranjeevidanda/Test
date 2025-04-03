using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NEC.Fulf3PL.Core.Common
{
    public class LogEventDetails
    {
        public string Provider { get; set; }
        public string RequestId { get; set; }
        public string EventName { get; set; }
        public Enums.TraceLogStatus Status { get; set; }
        public object Payload { get; set; }
        public object RequestHeader { get; set; }
        public HttpStatusCode httpStatusCode  { get; set; }
        public int? RetryCount { get; set; }
        public string AdditionaInfo { get; set; }
    }
}
