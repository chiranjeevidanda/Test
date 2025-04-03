using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEC.Fulf3PL.Core.Entities
{
    public class SubscriptionExtensionLog
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "resource")]
        public string Resource { get; set; }
        [JsonProperty(PropertyName = "notificationURL")]
        public string NotificationURL { get; set; }
        [JsonProperty(PropertyName = "subscriptionId")]
        public string SubscriptionId { get; set; }
        [JsonProperty(PropertyName = "odataetag")]
        public string Odataetag { get; set; }
        [JsonProperty(PropertyName = "loggedOn")]
        public DateTime LoggedOn { get; set; }

        [JsonProperty(PropertyName = "comments")]
        public string Comments { get; set; }
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }
        [JsonProperty(PropertyName = "expiryOn")]
        public string ExpiryOn { get; set; }
    }
}
