using Newtonsoft.Json;

namespace NEC.Fulf3PL.Application.Outbound.Implementation.COMMON.DTO
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class MasterDataResponseDTO
    {
        [JsonProperty("systemId")]
        public string SystemId { get; set; }

        [JsonProperty("customer")]
        public string Customer { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("activityId")]
        public string ActivityId { get; set; }
    }
}
