using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NEC.Fulf3PL.Application.Inbound.Implementation.KTN.Model
{
    public class WebhookSubsciptionsGetResponse
    {
        [JsonProperty("@odata.context")]
        public string ODataContext { get; set; }

        [JsonProperty("value")]
        public List<WebhookSubscriptionData> Subscriptions { get; set; }
    }

    public class WebhookSubscriptionData
    {
        [JsonProperty("@odata.etag")]
        public string ETag { get; set; }

        [JsonProperty("subscriptionId")]
        public string SubscriptionId { get; set; }

        [JsonProperty("notificationUrl")]
        public string NotificationUrl { get; set; }

        [JsonProperty("resource")]
        public string Resource { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("lastModifiedDateTime")]
        public DateTime LastModifiedDateTime { get; set; }

        [JsonProperty("clientState")]
        public string ClientState { get; set; }

        [JsonProperty("expirationDateTime")]
        public DateTime ExpirationDateTime { get; set; }

        [JsonProperty("systemCreatedAt")]
        public DateTime SystemCreatedAt { get; set; }

        [JsonProperty("systemCreatedBy")]
        public string SystemCreatedBy { get; set; }

        [JsonProperty("systemModifiedAt")]
        public DateTime SystemModifiedAt { get; set; }

        [JsonProperty("systemModifiedBy")]
        public string SystemModifiedBy { get; set; }
    }
}
