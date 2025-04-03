using Newtonsoft.Json;

namespace NEC.Fulf3PL.Application.Inbound.Implementation.KTN.DTO
{
    public class TransportOutboundLinksDTO
    {
        [JsonProperty("value")]
        public List<TransportLinks> Value { get; set; }
    }

    public class TransportOutboundLinks
    {
        [JsonProperty("systemId")]
        public string SystemId { get; set; }

        [JsonProperty("transportId")]
        public string TransportId { get; set; }

        [JsonProperty("documentId")]
        public string DocumentId { get; set; }
    }
}
