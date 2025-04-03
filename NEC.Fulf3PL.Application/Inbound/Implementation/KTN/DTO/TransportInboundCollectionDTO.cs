using Newtonsoft.Json;

namespace NEC.Fulf3PL.Application.Inbound.Implementation.KTN.DTO
{
    public class TransportInboundCollectionDTO
    {
        [JsonProperty("value")]
        public List<TransportInboundCollection> Value { get; set; }
    }

    public class TransportInboundCollection
    {
        [JsonProperty("systemId")]
        public string SystemId { get; set; }

        [JsonProperty("lastModifiedDateTime")]
        public string LastModifiedDateTime { get; set; }

        [JsonProperty("auxiliaryIndex1")]
        public int AuxiliaryIndex1 { get; set; }
    }
}
