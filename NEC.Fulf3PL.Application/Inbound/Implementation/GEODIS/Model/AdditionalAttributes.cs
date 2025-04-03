using Newtonsoft.Json;

namespace NEC.Fulf3PL.Application.Inbound.Implementation.GODISE.Model
{
    public class AdditionalAttributes
    {
        [JsonProperty("attributeName")]
        public string AttributeName { get; set; }

        [JsonProperty("attributeValue")]
        public string AttributeValue { get; set; }
    }
}