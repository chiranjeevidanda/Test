using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEC.Fulf3PL.Core.Common
{
    public static class LogEventExtension
    {
        public static bool Log(this LogEventDetails logdata, ILogger logger)
        {
            var result = false;
            Dictionary<string, object> logInfomation = GetLogInformation(logdata);
            using (logger.BeginScope(logInfomation))
            {
                logger.LogInformation(new EventId(GenerateUniqueNumber(), logdata.EventName), logdata.Status.ToString());
            }
            result = true;
            return result;
        }

        private static Dictionary<string, object> GetLogInformation(LogEventDetails logdata)
        {
        var logInfomation = new Dictionary<string, object>
            {
                ["Provider"] = logdata.Provider,
                ["RequestId"] = logdata.RequestId,
                ["Stage"] = logdata.Status.ToString(),
                ["Payload"] = logdata.Payload,
                ["RequestHeader"] = logdata.RequestHeader,
                ["HttpStatusCode"] = logdata.httpStatusCode.ToString(),
                ["RetryCount"] = logdata.RetryCount,
                ["AdditionaInfo"] = logdata.AdditionaInfo,
            };
            if (!string.IsNullOrEmpty(logdata.Provider))
            {
                logInfomation.Add("Provider", logdata.Provider);
            }
            if (!string.IsNullOrEmpty(logdata.RequestId))
            {
                logInfomation.Add("RequestId", logdata.RequestId);
            }
            if (logdata.RetryCount.HasValue)
            {
                logInfomation.Add("RetryCount", logdata.RetryCount.Value);
            }
            if (!string.IsNullOrEmpty(logdata.AdditionaInfo))
            {
                logInfomation.Add("AdditionaInfo", logdata.AdditionaInfo);
            }
            if (logdata.RequestHeader != null)
            {
                logInfomation.Add("RequestHeader", logdata.RequestHeader);
            }
            if (!string.IsNullOrEmpty(logdata.httpStatusCode.ToString()))
            {
                logInfomation.Add("HttpStatusCode", logdata.httpStatusCode.ToString());
            }

            return logInfomation;
        }
        private static int GenerateUniqueNumber()
        {
            string stringNumber = String.Format("{0:d9}", (DateTime.Now.Ticks / 10) % 1000000000);
            int result = Convert.ToInt32(stringNumber);
            return result;
        }
    }
}
