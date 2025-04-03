using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NEC.Fulf3PL.Application.Common.KTN.Interface;
using NEC.Fulf3PL.Application.Inbound.Implementation.KTN;
using NEC.Fulf3PL.Application.Inbound.Interface.KTN;
using NEC.Fulf3PL.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NEC.Fulf3PL.Core.Common.Enums;

namespace NEC.Fulf3PL.Application.Common.KTN
{
    public class WebhookSubscriptionFactory : IWebhookSubscriptionFactory
    {
        private readonly ILogger<WebhookSubscriptionFactory> logger;

        private readonly IServiceProvider serviceProvider;

        public WebhookSubscriptionFactory(IServiceProvider serviceProvider, ILogger<WebhookSubscriptionFactory> logger)
        {
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        public IWebhookSubscription CreateService(WebhookSubscriptionType type)
        {
            try
            {
                switch (type)
                {
                    case WebhookSubscriptionType.Outbound:
                        return serviceProvider.GetService<OutboundWebSubscription>() ?? throw new ArgumentNullException(nameof(OutboundWebSubscription));
                    case WebhookSubscriptionType.TransportInbound:
                        return serviceProvider.GetService<TransportOutboundWebSubscription>() ?? throw new ArgumentNullException(nameof(TransportOutboundWebSubscription));
                    case WebhookSubscriptionType.Inbound:
                        return serviceProvider.GetService<InboundWebSubscription>() ?? throw new ArgumentNullException(nameof(InboundWebSubscription));
                    case WebhookSubscriptionType.TransportOutbound:
                        return serviceProvider.GetService<TransportOutboundWebSubscription>() ?? throw new ArgumentNullException(nameof(TransportOutboundWebSubscription));
                    case WebhookSubscriptionType.ReturnReceipt:
                        return serviceProvider.GetService<ReturnReceiptWebSubscription>() ?? throw new ArgumentNullException(nameof(ReturnReceiptWebSubscription));
                    default:
                        throw new ArgumentException("Invalid logger type");
                }
            }
            catch (Exception ex)
            {
                // Handle the exception here
                logger.LogError(ex, "Error creating webhook subscription");
                throw; // Rethrow the exception to propagate it further if needed
            }
        }
        //string.Format("You selected {0}.", fruit)
        public IWebhookSubscription CreateService(string webhookResource, string companyId)
        {
            IWebhookSubscription service = serviceProvider.GetService<TransportInboundWebSubscription>();
            try
            {
               
                string inboundWebhookResource = string.Format(Constants.InboundWebhookResource, companyId);
                string transportsInboundWebhookResource = string.Format(Constants.TransportsInboundWebhookResource, companyId);
                string transportsOutboundWebhookResource = string.Format(Constants.TransportsOutboundWebhookResource, companyId);
                string outboundWebhookResource = string.Format(Constants.OutboundWebhookResource, companyId);
                string returnsWebhookResource = string.Format(Constants.ReturnsWebhookResource, companyId);

                Dictionary<string, Action> actions = new Dictionary<string, Action>
                {
                    { inboundWebhookResource, () => {
                        service = serviceProvider.GetService<InboundWebSubscription>() ?? throw new ArgumentNullException(nameof(InboundWebSubscription));
                    } },
                    { transportsInboundWebhookResource, () => {
                        service = serviceProvider.GetService<TransportInboundWebSubscription>() ?? throw new ArgumentNullException(nameof(TransportInboundWebSubscription));
                    } },
                    { transportsOutboundWebhookResource, () => {
                        service = serviceProvider.GetService<TransportOutboundWebSubscription>() ?? throw new ArgumentNullException(nameof(TransportOutboundWebSubscription));
                    } },
                    { outboundWebhookResource, () => {
                         service = serviceProvider.GetService<OutboundWebSubscription>() ?? throw new ArgumentNullException(nameof(OutboundWebSubscription));
                    } },
                    { returnsWebhookResource, () => {
                        service = serviceProvider.GetService<ReturnReceiptWebSubscription>() ?? throw new ArgumentNullException(nameof(ReturnReceiptWebSubscription));
                    } }
                };
                if (actions.TryGetValue(webhookResource, out Action? action))
                {
                    action?.Invoke();
                }
                else
                {
                    service = serviceProvider.GetService<TransportInboundWebSubscription>() ?? throw new ArgumentNullException(nameof(TransportInboundWebSubscription));
                }
            }
            catch (Exception ex)
            {
                // Handle the exception here
                logger.LogError(ex, "Error creating webhook subscription");
                throw; // Rethrow the exception to propagate it further if needed
            }

            return service;
        }
    }
}
