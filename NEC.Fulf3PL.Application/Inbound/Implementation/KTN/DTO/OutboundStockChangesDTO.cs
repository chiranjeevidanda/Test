using Newtonsoft.Json;

namespace NEC.Fulf3PL.Application.Inbound.Implementation.KTN.DTO
{

    public class OutboundStockChangesDTO
    {
        [JsonProperty("value")]
        public List<OutboundStock> OutboundStock { get; set; }
    }
    public class OutboundStock
    {
        [JsonProperty("transportId")]
        public string TransportId { get; set; }

        [JsonProperty("documentId")]
        public string DocumentId { get; set; }

        [JsonProperty("documentLineId")]
        public string DocumentLineId { get; set; }

        [JsonProperty("documentLineReference")]
        public string DocumentLineReference { get; set; }

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

        [JsonProperty("documentReference")]
        public string DocumentReference { get; set; }

        [JsonProperty("parcelId")]
        public string ParcelId { get; set; }

        [JsonProperty("systemId")]
        public string SystemId { get; set; }
    }
}
