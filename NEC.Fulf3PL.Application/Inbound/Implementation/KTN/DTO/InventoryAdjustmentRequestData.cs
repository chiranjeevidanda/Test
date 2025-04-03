
using NEC.Fulf3PL.Core.DTO;
using Newtonsoft.Json;

namespace NEC.Fulf3PL.Application.Inbound.Implementation.KTN.DTO
{
    public class InventoryAdjustmentRequestData
    {
        [JsonProperty("provider", NullValueHandling = NullValueHandling.Ignore)]
        public string Provider { get; set; }

        [JsonProperty("timestamp", NullValueHandling = NullValueHandling.Ignore)]
        public string TimeStamp { get; set; }

        [JsonProperty("requestId", NullValueHandling = NullValueHandling.Ignore)]
        public string RequestId { get; set; }

        [JsonProperty("adjustmentNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string AdjustmentNumber { get; set; }

        [JsonProperty("direction", NullValueHandling = NullValueHandling.Ignore)]
        public string Direction { get; set; }

        [JsonProperty("plant", NullValueHandling = NullValueHandling.Ignore)]
        public string Plant { get; set; }

        [JsonProperty("adjustmentDate", NullValueHandling = NullValueHandling.Ignore)]
        public string AdjustmentDate { get; set; }

        [JsonProperty("productCode", NullValueHandling = NullValueHandling.Ignore)]
        public string ProductCode { get; set; }

        [JsonProperty("operation", NullValueHandling = NullValueHandling.Ignore)]
        public string Operation { get; set; }

        [JsonProperty("change", NullValueHandling = NullValueHandling.Ignore)]
        public string Change { get; set; }

        [JsonProperty("movementType", NullValueHandling = NullValueHandling.Ignore)]
        public string MovementType { get; set; }

        [JsonProperty("reasonForMovement", NullValueHandling = NullValueHandling.Ignore)]
        public string ReasonForMovement { get; set; }

        [JsonProperty("adjustmentReasonText", NullValueHandling = NullValueHandling.Ignore)]
        public string AdjustmentReasonText { get; set; }

        [JsonProperty("uom", NullValueHandling = NullValueHandling.Ignore)]
        public string UOM { get; set; }

        [JsonProperty("additionalData")]
        public List<AdditionalDataDTO> AdditionalData { get; set; }
    }

}