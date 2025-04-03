using Newtonsoft.Json;

namespace NEC.Fulf3PL.Core.DTO.Admin.Geodis
{
    public class GeodisRequestPayloadDto
    {
        [JsonProperty("provider", NullValueHandling = NullValueHandling.Ignore)]
        public string? Provider { get; set; }

        [JsonProperty("requestId", NullValueHandling = NullValueHandling.Ignore)]
        public string? RequestId { get; set; }

        [JsonProperty("timestamp", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Timestamp { get; set; }

        [JsonProperty("orderNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string? OrderNumber { get; set; }

        [JsonProperty("deliveryNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string? DeliveryNumber { get; set; }

        [JsonProperty("vendorId", NullValueHandling = NullValueHandling.Ignore)]
        public string? VendorId { get; set; }

        [JsonProperty("receiptDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? ReceiptDate { get; set; }

        [JsonProperty("additionalData", NullValueHandling = NullValueHandling.Ignore)]
        public List<object>? AdditionalData { get; set; }

        [JsonProperty("entries", NullValueHandling = NullValueHandling.Ignore)]
        public List<object>? Entries { get; set; }

        [JsonProperty("adjustmentNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string? AdjustmentNumber { get; set; }

        [JsonProperty("direction", NullValueHandling = NullValueHandling.Ignore)]
        public string? Direction { get; set; }

        [JsonProperty("plant", NullValueHandling = NullValueHandling.Ignore)]
        public string? Plant { get; set; }

        [JsonProperty("adjustmentDate", NullValueHandling = NullValueHandling.Ignore)]
        public string? AdjustmentDate { get; set; }

        [JsonProperty("productCode", NullValueHandling = NullValueHandling.Ignore)]
        public string? ProductCode { get; set; }

        [JsonProperty("change", NullValueHandling = NullValueHandling.Ignore)]
        public string? Change { get; set; }

        [JsonProperty("movementType", NullValueHandling = NullValueHandling.Ignore)]
        public string? MovementType { get; set; }

        [JsonProperty("reasonForMovement", NullValueHandling = NullValueHandling.Ignore)]
        public string? ReasonForMovement { get; set; }

        [JsonProperty("adjustmentReasonText", NullValueHandling = NullValueHandling.Ignore)]
        public string? AdjustmentReasonText { get; set; }

        [JsonProperty("customerOrderNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string? CustomerOrderNumber { get; set; }

        [JsonProperty("orderDate", NullValueHandling = NullValueHandling.Ignore)]
        public string OrderDate { get; set; }

        [JsonProperty("storeNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string? StoreNumber { get; set; }

        [JsonProperty("deptNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string? DeptNumber { get; set; }

        [JsonProperty("shippingInfo", NullValueHandling = NullValueHandling.Ignore)]
        public ShippingDetailsRequestDTO ShippingInfo { get; set; }

        [JsonProperty("returnNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string ReturnNumber { get; set; }
    }
}
