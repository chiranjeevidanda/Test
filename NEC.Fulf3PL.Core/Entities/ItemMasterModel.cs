using NEC.Fulf3PL.Core.DTO;
using Newtonsoft.Json;

namespace NEC.Fulf3PL.Core.Entities
{
    public class ItemMasterModel
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "provider")]
        public string Provider { get; set; }

        [JsonProperty(PropertyName = "loggedOn")]
        public DateTime LoggedOn { get; set; }

        [JsonProperty(PropertyName = "updatedOn")]
        public DateTime UpdatedOn { get; set; }

        [JsonProperty(PropertyName = "data")]
        public ProductMasterRequestDTO Data { get; set; }

        [JsonProperty(PropertyName = "comments")]
        public string Comments { get; set; }

        [JsonProperty(PropertyName = "3PLDelivered")]
        public bool DeliveredToProvider { get; set; }
    }
}
