using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NEC.Fulf3PL.Application.Common.KTN.DTO;
using NEC.Fulf3PL.Application.Common.KTN.Interface;
using NEC.Fulf3PL.Application.Inbound.Implementation.KTN.Model;
using NEC.Fulf3PL.Application.Inbound.Interface.KTN;
using NEC.Fulf3PL.Core.Common;
using NEC.Fulf3PL.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEC.Fulf3PL.Application.Inbound.Implementation.KTN
{
    public class TransportInboundWebSubscription : WebhookSubscription, IWebhookSubscription
    {
        private readonly ILogger<WebhookSubscription> logger;

        public TransportInboundWebSubscription(ILogger<WebhookSubscription> logger, IConfiguration configuration, ITokenGenerationService<GenerateTokenDTO> iTokenGenerationService, IRequestHandler requestHandler)
        : base(logger, configuration, iTokenGenerationService, requestHandler)
        {
            this.logger = logger;
        }

        public override async Task<ExecuteResult<WebhookSubsciptionsPatchResponse>> ExtendSubscription(WebhookSubscriptionData subscription, Enums.WebhookSubscriptionType key, string customer = Constants.NEWERAB2B)
        {
            var result = new ExecuteResult<WebhookSubsciptionsPatchResponse>();
            result = await InvokeExtension(subscription, key, customer);
            logger.LogInformation($"Successfully extended {nameof(TransportInboundWebSubscription)}");
            return result;
        }
        //#region Dispose
        //private bool disposed = false;

        //// Implement IDisposable.
        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}

        //// Dispose(bool disposing) executes in two distinct scenarios:
        //// If disposing equals true, the method has been called directly
        //// or indirectly by a user's code. Managed and unmanaged resources
        //// can be disposed.
        //// If disposing equals false, the method has been called by the
        //// runtime from inside the finalizer and you should not reference
        //// other objects. Only unmanaged resources can be disposed.
        //protected virtual void Dispose(bool disposing)
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
        //}

        //// Finalizer to ensure that unmanaged resources get released if Dispose() is not called.
        //~TransportInboundWebSubscription()
        //{
        //    Dispose(false);
        //}
        //#endregion Dispose
    }
}
