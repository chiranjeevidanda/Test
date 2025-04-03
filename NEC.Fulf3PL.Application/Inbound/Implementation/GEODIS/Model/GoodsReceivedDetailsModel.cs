using Newtonsoft.Json;

namespace NEC.Fulf3PL.Application.Inbound.Implementation.GODISE.Model
{
    public class GoodsReceivedDetailsModel
    {

        [JsonProperty("eventId")]
        public string EventId { get; set; }

        [JsonProperty("customer")]
        public string Customer { get; set; }

        [JsonProperty("documentNumber")]
        public string DocumentNumber { get; set; }

        [JsonProperty("shipmentId")]
        public string ShipmentId { get; set; }

        [JsonProperty("purchaseOrderNumber")]
        public string PurchaseOrderNumber { get; set; }

        [JsonProperty("actualReceiptDate")]
        public string ActualReceiptDate { get; set; }

        [JsonProperty("actualReceiptTime")]
        public string ActualReceiptTime { get; set; }

        [JsonProperty("vendorId")]
        public string VendorId { get; set; }

        [JsonProperty("receivingWarehouseInfo")]
        public ReceivingWarehouseInfo WarehouseInfo { get; set; }

        [JsonProperty("lines")]
        public List<POItemDetailsModel> Lines { get; set; }

        [JsonProperty("additionalAttributes")]
        public List<AdditionalAttributes> AdditionalAttributes { get; set; }

    }

    public class POItemDetailsModel
    {
        [JsonProperty("materialNumber")]
        public string MaterialNumber { get; set; }

        [JsonProperty("customerLineNumber")]
        public string CustomerLineNumber { get; set; }

        [JsonProperty("upcCode")]
        public string UPCCode { get; set; }

        [JsonProperty("quantityReceived")]
        public int QuantityReceived { get; set; }

        [JsonProperty("quantityUom")]
        public string QuantityUom { get; set; }

        [JsonProperty("receivingWarehouseLocation")]
        public string ReceivingWarehouseLocation { get; set; }

        [JsonProperty("additionalAttributes")]
        public List<AdditionalAttributes> AdditionalAttributes { get; set; }
    }

    public class ReceivingWarehouseInfo
    {
        [JsonProperty("warehouseId")]
        public string WarehouseId { get; set; }

        [JsonProperty("addressLine1")]
        public string AddressLine1 { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("postalCode")]
        public string PostalCode { get; set; }

        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }
    }
}
