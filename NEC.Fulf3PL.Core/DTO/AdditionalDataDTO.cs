using Newtonsoft.Json;

namespace NEC.Fulf3PL.Core.DTO
{
    public class AdditionalDataDTO
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
