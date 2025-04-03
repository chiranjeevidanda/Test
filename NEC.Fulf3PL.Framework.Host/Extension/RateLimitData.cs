using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEC.Fulf3PL.Framework.Host.Extension
{
    public class RateLimitData
    {
        public string Name { get; set; }
        public int PeriodInSeconds { get; set; }
        public int Limit { get; set; }
        public string Endpoint { get; set; }
    }
}
