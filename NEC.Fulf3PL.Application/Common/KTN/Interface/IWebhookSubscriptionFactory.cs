using NEC.Fulf3PL.Application.Inbound.Interface.KTN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NEC.Fulf3PL.Core.Common.Enums;

namespace NEC.Fulf3PL.Application.Common.KTN.Interface
{
    public interface IWebhookSubscriptionFactory
    {
        //IWebhookSubscription CreateService(WebhookSubscriptionType type);
        IWebhookSubscription CreateService(string webhookResource, string companyId);
    }
}
