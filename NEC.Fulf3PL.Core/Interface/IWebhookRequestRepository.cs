using NEC.Fulf3PL.Core.Entities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEC.Fulf3PL.Core.Interface
{
    public interface IWebhookRequestRepository : ICosmosRepository<WebhookRequest>
    {
    }
}
