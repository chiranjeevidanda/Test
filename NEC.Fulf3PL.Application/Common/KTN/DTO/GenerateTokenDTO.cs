using Newtonsoft.Json;

namespace NEC.Fulf3PL.Application.Common.KTN.DTO
{
    public class GenerateTokenDTO
    {
        [JsonProperty("access_token")]
        public string Token { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public string ExpiresIn { get; set; }

        [JsonProperty("ext_expires_in")]
        public string Ext_ExpiresIn { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        public string KeyName { get; set; }
        public string KeyValue { get; set; }
    }
}
