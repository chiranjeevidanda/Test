using NEC.Fulf3PL.Application.Inbound.Implementation.KTN.Model;
using NEC.Fulf3PL.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEC.Fulf3PL.Application.Inbound.Interface.KTN
{
    /// Represents a webhook subscription.
    /// </summary>
    public interface IWebhookSubscription
    {
        /// <summary>
        /// Extends the subscription.
        /// </summary>
        /// <returns>The result of the subscription extension.</returns>
        Task<ExecuteResult<WebhookSubsciptionsPatchResponse>> ExtendSubscription(WebhookSubscriptionData subscription, Enums.WebhookSubscriptionType key, string customer = Constants.NEWERAB2B);
        Task<ExecuteResult<WebhookSubsciptionsGetResponse>> GetWebhookSubscription(Enums.WebhookSubscriptionType key,string companyId);
    }
}
