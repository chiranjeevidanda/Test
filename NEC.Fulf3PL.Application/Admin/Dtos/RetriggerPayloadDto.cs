using System.Text.Json.Serialization;

namespace NEC.Fulf3PL.Application.Admin.Dtos;

public class RetriggerPayloadDto
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("requestPayload")]
    public string? RequestPayload { get; set; }
}
