using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace NEC.Fulf3PL.Core.Entities.Admin;

public class TransactionLogResponseModel
{
    [JsonPropertyName("systemId")]
    public string? SystemId { get; set; }
}