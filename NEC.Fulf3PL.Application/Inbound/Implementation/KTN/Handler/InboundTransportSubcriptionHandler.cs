using MediatR;
using Microsoft.Extensions.Logging;
using NEC.Fulf3PL.Application.Common.KTN.Commands;
using NEC.Fulf3PL.Application.Common.KTN.Interface;
using NEC.Fulf3PL.Application.Inbound.Implementation.KTN.Model;
using NEC.Fulf3PL.Core.Common;
using NEC.Fulf3PL.Core.Entities;
using NEC.Fulf3PL.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NEC.Fulf3PL.Application.Common.KTN.Commands.SubcriptionExtendCommand;

namespace NEC.Fulf3PL.Application.Inbound.Implementation.KTN.Handler
{
    public class InboundTransportSubcriptionHandler : IRequestHandler<InboundTransportSubcriptionExtCommand, WebhookSubsciptionsPatchResponse>
    {
        private readonly IWebhookSubscriptionFactory webhookFactory;
        private readonly ILogger<InboundTransportSubcriptionHandler> logger;
        private readonly ISubscriptionExtensionLogRepository subcriptionRepository;

        public InboundTransportSubcriptionHandler(IWebhookSubscriptionFactory webhookFactory, ILogger<InboundTransportSubcriptionHandler> logger, ISubscriptionExtensionLogRepository subcriptionRepository)
        {
            this.webhookFactory = webhookFactory;
            this.logger = logger;
            this.subcriptionRepository = subcriptionRepository;
        }

        public async Task<WebhookSubsciptionsPatchResponse> Handle(InboundTransportSubcriptionExtCommand request, CancellationToken cancellationToken)
        {

            logger.LogInformation("Saying hello to {name}.", request.subsciptionsParams.Item1);
            var webhookService = webhookFactory.CreateService(request.subsciptionsParams.Item2, request.subsciptionsParams.Item3);
            var result = await webhookService.ExtendSubscription(request.subsciptionsParams.Item1, Enums.WebhookSubscriptionType.TransportInbound, request.subsciptionsParams.Item4);
            await PostLogDetails(request.subsciptionsParams.Item1);
            return result.Result;
        }
        private async Task PostLogDetails(WebhookSubscriptionData subscription)
        {
            await subcriptionRepository.CreateAsync(new SubscriptionExtensionLog()
            {
                Id = Guid.NewGuid().ToString(),
                Resource = subscription.Resource,
                SubscriptionId = subscription.SubscriptionId,
                LoggedOn = DateTime.UtcNow,
                Odataetag = subscription.ETag,
                NotificationURL = subscription.NotificationUrl,
                Status = "Success",
                ExpiryOn = subscription.ExpirationDateTime.ToString(),
                Comments = string.Empty
            });
        }
    }
}
