using Newtonsoft.Json;

namespace NEC.Fulf3PL.Application.Outbound.Implementation.GEODIS.DTO
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class ReturnOrderDTO
    {
        [JsonProperty("eventId")]
        public string EventId { get; set; }

        [JsonProperty("transaction")]
        public string Transaction { get; set; }

        [JsonProperty("customer")]
        public string Customer { get; set; }

        [JsonProperty("returnNumber")]
        public string ReturnNumber { get; set; }

        [JsonProperty("returnCreationDate")]
        public string ReturnCreationDate { get; set; }

        [JsonProperty("purchaseOrderNumber")]
        public string PurchaseOrderNumber { get; set; }

        [JsonProperty("partnerId")]
        public string PartnerId { get; set; }

        [JsonProperty("vendorId")]
        public string VendorId { get; set; }

        [JsonProperty("salesOrder")]
        public string SalesOrder { get; set; }

        [JsonProperty("soldToName")]
        public string SoldToName { get; set; }

        [JsonProperty("soldToNumber")]
        public string SoldToNumber { get; set; }

        [JsonProperty("orderHeaderReason")]
        public string OrderHeaderReason { get; set; }

        [JsonProperty("lines")]
        public List<ReturnLineDetailsDTO> Lines { get; set; }
    }

    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class ReturnLineDetailsDTO
    {
        [JsonProperty("materialNumber")]
        public string MaterialNumber { get; set; }

        [JsonProperty("upcCode")]
        public string UPCCode { get; set; }

        [JsonProperty("customerLineNumber")]
        public string CustomerLineNumber { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("unitOfMeasure")]
        public string UnitOfMeasure { get; set; }

        [JsonProperty("countryOfOrigin")]
        public string CountryOfOrigin { get; set; }

        [JsonProperty("receivingWarehouseLocation")]
        public string ReceivingWarehouseLocation { get; set; }

    }
}
