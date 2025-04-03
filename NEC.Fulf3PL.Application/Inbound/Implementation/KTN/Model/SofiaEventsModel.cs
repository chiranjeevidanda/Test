using Newtonsoft.Json;

namespace NEC.Fulf3PL.Application.Inbound.Implementation.KTN.Model
{
    public class SofiaEventsModel
    {

        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("eventType")]
        public string EventType { get; set; }

        [JsonProperty("eventTime")]
        public string EventTime { get; set; }

        [JsonProperty("data")]
        public EventData Data { get; set; }

        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }
    }

    public class EventData
    {

        [JsonProperty("orderName", NullValueHandling = NullValueHandling.Ignore)]
        public string OrderName { get; set; }

        [JsonProperty("warehouseProvider", NullValueHandling = NullValueHandling.Ignore)]
        public string WarehouseProvider { get; set; }

        [JsonProperty("adjustmentNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string AdjustmentNumber { get; set; }

        [JsonProperty("returnReference", NullValueHandling = NullValueHandling.Ignore)]
        public string ReturnReference { get; set; }

        [JsonProperty("trackingNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string TrackingNumber { get; set; }

        [JsonProperty("trackingUrl", NullValueHandling = NullValueHandling.Ignore)]
        public string TrackingUrl { get; set; }

        [JsonProperty("shippingCarrier", NullValueHandling = NullValueHandling.Ignore)]
        public string ShippingCarrier { get; set; }

        [JsonProperty("shippedDate", NullValueHandling = NullValueHandling.Ignore)]
        public string ShippedDate { get; set; }

        [JsonProperty("reason", NullValueHandling = NullValueHandling.Ignore)]
        public string Reason { get; set; }

        [JsonProperty("lineItems", NullValueHandling = NullValueHandling.Ignore)]
        public List<OrderEventLines> LineItems { get; set; }
    }

    public class OrderEventLines
    {

        [JsonProperty("sku")]
        public string SKU { get; set; }

        [JsonProperty("quantity", NullValueHandling = NullValueHandling.Ignore)]
        public int? Quantity { get; set; }

        [JsonProperty("adjustmentQuantity", NullValueHandling = NullValueHandling.Ignore)]
        public int? AdjustmentQuantity { get; set; }
    }
}
