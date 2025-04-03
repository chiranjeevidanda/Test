using NEC.Fulf3PL.Core.DTO;
using Newtonsoft.Json;

namespace NEC.Fulf3PL.Application.Inbound.Implementation.KTN.DTO
{
    public class ReturnReceivedRequestData
    {
        [JsonProperty("provider", NullValueHandling = NullValueHandling.Ignore)]
        public string Provider { get; set; }

        [JsonProperty("requestId", NullValueHandling = NullValueHandling.Ignore)]
        public string RequestId { get; set; }

        [JsonProperty("timestamp", NullValueHandling = NullValueHandling.Ignore)]
        public string TimeStamp { get; set; }

        [JsonProperty("orderNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string OrderNumber { get; set; }

        [JsonProperty("returnNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string ReturnNumber { get; set; }

        [JsonProperty("vendorId", NullValueHandling = NullValueHandling.Ignore)]
        public string VendorId { get; set; }

        [JsonProperty("plant", NullValueHandling = NullValueHandling.Ignore)]
        public string Plant { get; set; }

        [JsonProperty("receiptDate", NullValueHandling = NullValueHandling.Ignore)]
        public string ReceiptDate { get; set; }

        [JsonProperty("additionalData")]
        public List<AdditionalDataDTO> AdditionalData { get; set; }

        [JsonProperty("entries")]
        public List<OrderEntryRequestDTO> Entries { get; set; }

    }

}