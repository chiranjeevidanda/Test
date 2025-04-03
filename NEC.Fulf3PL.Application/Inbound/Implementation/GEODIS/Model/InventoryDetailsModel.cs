using Newtonsoft.Json;

namespace NEC.Fulf3PL.Application.Inbound.Implementation.GEODIS.Model
{
    public class InventoryDetailsModel
    {

        [JsonProperty("eventId")]
        public string EventID { get; set; }

        [JsonProperty("customer")]
        public string Customer { get; set; }

        [JsonProperty("adjustmentDate")]
        public string AdjustmentDate { get; set; }

        [JsonProperty("adjustmentNumber")]
        public string AdjustmentNumber { get; set; }

        [JsonProperty("vendorNumber")]
        public string VendorNumber { get; set; }

        [JsonProperty("lineItem")]
        public InventoryAdjustmentLineItem InventoryAdjustmentLineItem { get; set; }
    }

    public class InventoryAdjustmentLineItem
    {

        [JsonProperty("adjustmentDirection")]
        public string AdjustmentDirection { get; set; }

        [JsonProperty("upcCode")]
        public string UPCCode { get; set; }

        [JsonProperty("customerLineNumber")]
        public string CustomerLineNumber { get; set; }

        [JsonProperty("adjustmentQuantity")]
        public string AdjustmentQuantity { get; set; }

        [JsonProperty("unitOfMeasure")]
        public string UnitOfMeasure { get; set; }

        [JsonProperty("partnerMovementType")]
        public string PartnerMovementType { get; set; }

        [JsonProperty("adjustmentReasonText")]
        public string AdjustmentReasonText { get; set; }

        [JsonProperty("reasonForMovement")]
        public string ReasonForMovement { get; set; }

        [JsonProperty("providerPlantName")]
        public string ProviderPlantName { get; set; }

        [JsonProperty("providerMovementType")]
        public string ProviderMovementType { get; set; }

    }
}
