using Newtonsoft.Json;

namespace NEC.Fulf3PL.Application.Inbound.Implementation.KTN.DTO
{
    public class TransportInboundStockChangeDTO
    {
        [JsonProperty("value")]
        public List<InboundStockChange> Value { get; set; }
    }

    public class InboundStockChange
    {
        [JsonProperty("transportId")]
        public string TransportId { get; set; }

        [JsonProperty("documentId")]
        public string DocumentId { get; set; }

        [JsonProperty("documentLineId")]
        public string DocumentLineId { get; set; }

        [JsonProperty("lineReference")]
        public string LineReference { get; set; }

        [JsonProperty("itemCode")]
        public string ItemCode { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("bestBeforeDate")]
        public string BestBeforeDate { get; set; }

        [JsonProperty("lotNo")]
        public string LotNo { get; set; }

        [JsonProperty("blockingCode")]
        public string BlockingCode { get; set; }

        [JsonProperty("parcelId")]
        public string ParcelId { get; set; }

        [JsonProperty("systemId")]
        public string SystemId { get; set; }
    }
}
