using Newtonsoft.Json;

namespace NEC.Fulf3PL.Core.Entities.Persistence;

public abstract class AuditableEntity : BaseEntity, IAuditableEntity

{
    [JsonProperty("loggedOn")]
    public DateTime LoggedOn { get; set; }

    [JsonProperty("modifiedDate")]
    public DateTime ModifiedDate { get; set; }
}
