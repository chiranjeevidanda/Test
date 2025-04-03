using NEC.Fulf3PL.Core.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEC.Fulf3PL.Application.Inbound.Implementation.KTN.Model
{
    // define constructor and initiate properties for WebhookExtensionModel
    public class WebhookExtensionModel
    {
        [JsonProperty("companyId")]
        public string CompanyId { get; set; }

        [JsonProperty("subscriptionId")]
        public string SubscriptionId { get; set; }

        [JsonProperty("subscriptionEtag")]
        public string SubscriptionEtag { get; set; }

        [JsonProperty("subscriptionkey")]
        public string SubscriptionKey { get; set; }
        [JsonProperty("customer")]
        public string Customer { get; set; }

        [JsonProperty("payload")]
        public WebhookExtensionPayload Payload { get; set; }

        public WebhookExtensionModel()
        {
            Payload = new WebhookExtensionPayload();
        }
        public Enums.WebhookSubscriptionType Type { get; set; }
    }
    // define class for WebhookExtensionPayload
    public class WebhookExtensionPayload
    {
        [JsonProperty("clientState")]
        public string ClientState { get; set; }
    }
}
