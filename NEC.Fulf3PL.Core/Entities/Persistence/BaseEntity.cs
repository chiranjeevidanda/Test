using Newtonsoft.Json;

namespace NEC.Fulf3PL.Core.Entities.Persistence;

public abstract class BaseEntity
{
    [JsonProperty("id")]
    public string Id { get; set; } = Guid.NewGuid().ToString();
}
