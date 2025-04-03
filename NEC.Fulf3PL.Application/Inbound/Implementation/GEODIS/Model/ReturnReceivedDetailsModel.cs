using Newtonsoft.Json;

namespace NEC.Fulf3PL.Application.Inbound.Implementation.GODISE.Model
{
    public class ReturnReceivedDetailsModel
    {

        [JsonProperty("eventId")]
        public string EventId { get; set; }

        [JsonProperty("customer")]
        public string Customer { get; set; }

        [JsonProperty("documentNumber")]
        public string DocumentNumber { get; set; }

        [JsonProperty("returnNumber")]
        public string ReturnNumber { get; set; }

        [JsonProperty("purchaseOrderNumber")]
        public string PurchaseOrderNumber { get; set; }

        [JsonProperty("purchaseOrderDate")]
        public string PurchaseOrderDate { get; set; }

        [JsonProperty("vendorId")]
        public string VendorId { get; set; }

        [JsonProperty("actualReceiptDate")]
        public string ActualReceiptDate { get; set; }

        [JsonProperty("actualReceiptTime")]
        public string ActualReceiptTime { get; set; }

        [JsonProperty("partnerId")]
        public string PartnerId { get; set; }

        [JsonProperty("soldToName")]
        public string SoldToName { get; set; }

        [JsonProperty("soldToNumber")]
        public string SoldToNumber { get; set; }

        [JsonProperty("salesOrder")]
        public string SalesOrder { get; set; }

        [JsonProperty("OrderHeaderReason")]
        public string orderHeaderReason { get; set; }

        [JsonProperty("lines")]
        public List<ReturnItemDetailsModel> Lines { get; set; }

        [JsonProperty("additionalAttributes")]
        public List<AdditionalAttributes> AdditionalAttributes { get; set; }

    }

    public class ReturnItemDetailsModel
    {
        [JsonProperty("materialNumber")]
        public string MaterialNumber { get; set; }

        [JsonProperty("customerLineNumber")]
        public string CustomerLineNumber { get; set; }

        [JsonProperty("upcCode")]
        public string UPCCode { get; set; }

        [JsonProperty("quantityReceived")]
        public int QuantityReceived { get; set; }

        [JsonProperty("unitOfMeasure")]
        public string UnitOfMeasure { get; set; }

        [JsonProperty("countryOfOrigin")]
        public string CountryOfOrigin { get; set; }

        [JsonProperty("receivingWarehouseLocation")]
        public string ReceivingWarehouseLocation { get; set; }

        [JsonProperty("additionalAttributes")]
        public List<AdditionalAttributes> AdditionalAttributes { get; set; }
    }

}
