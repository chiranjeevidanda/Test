using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEC.Fulf3PL.Application.Outbound.Implementation.COMMON.Helpers
{
    public class DeliveryDataModel
    {
        public string Data { get; set; }
        public ProcessConfiguration Configurations { get; set; }
    }

    public class ProcessConfiguration
    {
        public int RetryDelaySeconds { get; set; }
        public int RetryMaxDelaySeconds { get; set; }
        public int RetryMaxRetries { get; set; }
        public int RetryBackoffCoefficient { get; set; }
        public string ProcessId { get; set; }


    }
}
