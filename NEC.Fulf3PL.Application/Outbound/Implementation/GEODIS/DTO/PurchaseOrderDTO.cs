using Newtonsoft.Json;

namespace NEC.Fulf3PL.Application.Outbound.Implementation.GEODIS.DTO
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class PurchaseOrderDTO
    {
        [JsonProperty("eventId")]
        public string EventId { get; set; }

        [JsonProperty("transaction")]
        public string Transaction { get; set; }

        [JsonProperty("customer")]
        public string Customer { get; set; }

        [JsonProperty("purchaseOrderNumber")]
        public string PurchaseOrderNumber { get; set; }

        [JsonProperty("purchaseOrderDate")]
        public string PurchaseOrderDate { get; set; }

        [JsonProperty("deliveryType")]
        public string DeliveryType { get; set; }

        [JsonProperty("deliveryDate")]
        public string DeliveryDate { get; set; }

        [JsonProperty("shipmentId")]
        public string ShipmentId { get; set; }

        [JsonProperty("vendorId")]
        public string VendorId { get; set; }

        [JsonProperty("partnerId")]
        public string PartnerId { get; set; }

        [JsonProperty("vendorName")]
        public string VendorName { get; set; }

        [JsonProperty("vendorCountry")]
        public string VendorCountry { get; set; }

        [JsonProperty("factoryDate")]
        public string FactoryDate { get; set; }

        [JsonProperty("lines")]
        public List<LineItemDetailsDTO> Lines { get; set; }
    }

    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class LineItemDetailsDTO
    {
        [JsonProperty("materialNumber")]
        public string MaterialNumber { get; set; }

        [JsonProperty("upcCode")]
        public string UPCCode { get; set; }

        [JsonProperty("customerLineNumber")]
        public string CustomerLineNumber { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("unitOfMeasure")]
        public string UnitOfMeasure { get; set; }

        [JsonProperty("uniqueIdentiifier")]
        public string UniqueIdentiifier { get; set; }

        [JsonProperty("ssccNumber")]
        public string SSCCNumber { get; set; }

        [JsonProperty("containerNumber")]
        public string ContainerNumber { get; set; }

        [JsonProperty("bolNumber")]
        public string BOLNumber { get; set; }

        [JsonProperty("soNumber")]
        public string SONumber { get; set; }

        [JsonProperty("soCustomerNumber")]
        public string SOCustomerNumber { get; set; }

        [JsonProperty("cartonNumber")]
        public string CartonNumber { get; set; }

        [JsonProperty("receivingWarehouseLocation")]
        public string ReceivingWarehouseLocation { get; set; }

    }
}
