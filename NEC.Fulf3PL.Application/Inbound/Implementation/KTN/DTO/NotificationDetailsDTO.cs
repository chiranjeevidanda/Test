using Newtonsoft.Json;

namespace NEC.Fulf3PL.Application.Inbound.Implementation.KTN.DTO
{
    public class NotificationDetailsDTO
    {
        [JsonProperty("value")]
        public List<NotificationDetails> Value { get; set; }
    }

    public class NotificationDetails
    {
        [JsonProperty("subscriptionId")]
        public string SubscriptionId { get; set; }

        [JsonProperty("clientState")]
        public string ClientState { get; set; }

        [JsonProperty("expirationDateTime")]
        public string ExpirationDateTime { get; set; }

        [JsonProperty("resource")]
        public string Resource { get; set; }

        [JsonProperty("changeType")]
        public string ChangeType { get; set; }

        [JsonProperty("lastModifiedDateTime")]
        public string LastModifiedDateTime { get; set; }
    }
}
