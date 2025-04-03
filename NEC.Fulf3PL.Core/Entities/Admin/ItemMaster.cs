using NEC.Fulf3PL.Core.DTO;
using NEC.Fulf3PL.Core.Entities.Persistence;
using Newtonsoft.Json;

namespace NEC.Fulf3PL.Core.Entities.Admin
{
    public class ItemMaster : AuditableEntity
    {
        [JsonProperty("provider")]
        public string? Provider { get; set; }

        [JsonProperty("updatedOn")]
        public DateTime UpdatedOn { get; set; }

        [JsonProperty("data")]
        public ProductMasterRequestDTO Data { get; set; }

        [JsonProperty("comments")]
        public string? Comments { get; set; }

        [JsonProperty("3PLDelivered")]
        public bool DeliveredToProvider { get; set; }
    }
}
