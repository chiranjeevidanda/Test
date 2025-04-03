using MediatR;
using Microsoft.Azure.Amqp.Framing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NEC.Fulf3PL.Application.Common.KTN;
using NEC.Fulf3PL.Application.Common.KTN.DTO;
using NEC.Fulf3PL.Application.Common.KTN.Interface;
using NEC.Fulf3PL.Application.Inbound.Implementation.KTN.Common;
using NEC.Fulf3PL.Application.Inbound.Implementation.KTN.Model;
using NEC.Fulf3PL.Application.Inbound.Interface.KTN;
using NEC.Fulf3PL.Core.Common;
using NEC.Fulf3PL.Core.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NEC.Fulf3PL.Application.Inbound.Implementation.KTN
{
    public class InboundWebSubscription : WebhookSubscription, IWebhookSubscription
    {
        private readonly ILogger<WebhookSubscription> logger;

        public InboundWebSubscription(ILogger<WebhookSubscription> logger, IConfiguration configuration, ITokenGenerationService<GenerateTokenDTO> iTokenGenerationService, IRequestHandler requestHandler)
        : base(logger, configuration, iTokenGenerationService, requestHandler)
        {
            this.logger = logger;
        }

        public override async Task<ExecuteResult<WebhookSubsciptionsPatchResponse>> ExtendSubscription(WebhookSubscriptionData subscription, Enums.WebhookSubscriptionType key, string customer = Constants.NEWERAB2B)
        {
            var result = new ExecuteResult<WebhookSubsciptionsPatchResponse>();
            result = await InvokeExtension(subscription, key, customer);
            logger.LogInformation($"Successfully extended {nameof(InboundWebSubscription)}");
            return result;
        }


        //protected void Dispose(bool disposing)
        //{
        //    if (!disposed)
        //    {
        //        if (disposing)
        //        {
        //            // Dispose managed resources
        //        }

        //        // Dispose unmanaged resources
        //        // Set large fields to null to free up resources.
        //        disposed = true;
        //    }
        //    Dispose(disposing);
        //}

        //private bool disposed = false;

        //public new void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}

        //~InboundWebSubscription()
        //{
        //    Dispose(false);
        //}
    }
}
