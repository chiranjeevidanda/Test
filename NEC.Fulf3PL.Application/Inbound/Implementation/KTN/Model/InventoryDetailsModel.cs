using Newtonsoft.Json;

namespace NEC.Fulf3PL.Application.Inbound.Implementation.KTN.Model
{
    public class InventoryDetailsModel
    {

        [JsonProperty("InventoryAdjustmentRequest")]
        public InventoryAdjustmentRequest InventoryAdjustmentRequest { get; set; }
    }

    public class InventoryAdjustmentRequest
    {

        [JsonProperty("EventID")]
        public string EventID { get; set; }

        [JsonProperty("SalesOrder")]
        public string SalesOrder { get; set; }

        [JsonProperty("AdjustmentDate")]
        public string AdjustmentDate { get; set; }

        [JsonProperty("AdjustmentNumber")]
        public string AdjustmentNumber { get; set; }

        [JsonProperty("MovementType")]
        public string MovementType { get; set; }

        [JsonProperty("VendorNumber")]
        public string VendorNumber { get; set; }

        [JsonProperty("InventoryAdjustmentLineItem")]
        public InventoryAdjustmentLineItem InventoryAdjustmentLineItem { get; set; }
    }

    public class InventoryAdjustmentLineItem
    {

        [JsonProperty("AdjustmentDirection")]
        public string AdjustmentDirection { get; set; }

        [JsonProperty("AdjustmentQuantity")]
        public string AdjustmentQuantity { get; set; }

        [JsonProperty("VendorPartNumber")]
        public string VendorPartNumber { get; set; }

        [JsonProperty("UPC")]
        public string UPC { get; set; }

        [JsonProperty("AdjustmentReasonText")]
        public string AdjustmentReasonText { get; set; }

        [JsonProperty("CustomerMovementReason")]
        public string CustomerMovementReason { get; set; }

        [JsonProperty("CustomerMovementReasonText")]
        public string CustomerMovementReasonText { get; set; }

        [JsonProperty("UOM")]
        public string UOM { get; set; }
    }
}
