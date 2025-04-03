using MediatR;
using NEC.Fulf3PL.Application.Inbound.Implementation.KTN.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEC.Fulf3PL.Application.Common.KTN.Commands
{
    public class SubcriptionExtendCommand
    {
        public record InboundSubcriptionExtCommand((WebhookSubscriptionData, string, string, string) subsciptionsParams) : IRequest<WebhookSubsciptionsPatchResponse>;
        public record InboundTransportSubcriptionExtCommand((WebhookSubscriptionData, string, string, string) subsciptionsParams) : IRequest<WebhookSubsciptionsPatchResponse>;
        public record OutboundTransportSubcriptionExtCommand((WebhookSubscriptionData, string, string, string) subsciptionsParams) : IRequest<WebhookSubsciptionsPatchResponse>;
        public record OutboundSubcriptionExtCommand((WebhookSubscriptionData, string, string, string) subsciptionsParams) : IRequest<WebhookSubsciptionsPatchResponse>;
        public record ReturnSubcriptionExtCommand((WebhookSubscriptionData, string, string, string) subsciptionsParams) : IRequest<WebhookSubsciptionsPatchResponse>;
    }
}
