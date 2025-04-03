using Newtonsoft.Json;

namespace NEC.Fulf3PL.Core.Entities
{
    public class OutboundItemsLogModel
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "provider")]
        public string Provider { get; set; }

        [JsonProperty(PropertyName = "requestType")]
        public string RequestType { get; set; }

        [JsonProperty(PropertyName = "orderNumber")]
        public string OrderNumber { get; set; }

        [JsonProperty(PropertyName = "itemCode")]
        public string ItemCode { get; set; }

        [JsonProperty(PropertyName = "lineReference")]
        public string LineReference { get; set; }

        [JsonProperty(PropertyName = "uom")]
        public string UOM { get; set; }

        [JsonProperty(PropertyName = "loggedOn")]
        public DateTime LoggedOn { get; set; }

        [JsonProperty(PropertyName = "comments")]
        public string Comments { get; set; }
    }
}
