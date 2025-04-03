using Newtonsoft.Json;

namespace NEC.Fulf3PL.Core.DTO
{
    public class PurchaseOrderRequestDTO
    {

        [JsonProperty("requestId")]
        public string RequestId { get; set; }

        [JsonProperty("timestamp")]
        public string TimeStamp { get; set; }

        [JsonProperty("vendorId")]
        public string VendorId { get; set; }

        [JsonProperty("orderNumber")]
        public string OrderNumber { get; set; }

        [JsonProperty("orderDate")]
        public string OrderDate { get; set; }

        [JsonProperty("deliveryNumber")]
        public string DeliveryNumber { get; set; }

        [JsonProperty("plant")]
        public string Plant { get; set; }

        [JsonProperty("additionalData")]
        public List<AdditionalDataDTO> AdditionalData { get; set; }

        [JsonProperty("entries")]
        public List<OrderEntryRequestDTO> Entries { get; set; }

    }

}