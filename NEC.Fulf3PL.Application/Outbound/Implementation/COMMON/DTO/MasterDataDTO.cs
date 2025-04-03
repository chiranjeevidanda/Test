using Newtonsoft.Json;

namespace NEC.Fulf3PL.Application.Outbound.Implementation.COMMON.DTO
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class MasterDataDTO
    {
        [JsonProperty("customer")]
        public string Customer { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
