using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace NEC.Fulf3PL.Core.Entities.Admin;

public class RequestPayloadModel
{
    [JsonPropertyName("customer")]
    public string? Customer { get; set; }

    [JsonPropertyName("code")]
    public string? Code { get; set; }

    [JsonPropertyName("destinationReference")]
    public string? DestinationReference { get; set; }

    [JsonPropertyName("assignmentReference")]
    public string? AssignmentReference { get; set; }

    [JsonPropertyName("customerReference3")]
    public string? CustomerReference3 { get; set; }

    [JsonPropertyName("customerReference2")]
    public string? CustomerReference2 { get; set; }

    [JsonPropertyName("customerReference4")]
    public string? CustomerReference4 { get; set; }

    [JsonPropertyName("customerReference5")]
    public string? CustomerReference5 { get; set; }

    [JsonPropertyName("incotermCode")]
    public string? IncotermCode { get; set; }

    [JsonPropertyName("operationType")]
    public string? OperationType { get; set; }

    [JsonPropertyName("deliveryDate")]
    public string? DeliveryDate { get; set; }

    [JsonPropertyName("addresses")]
    public object Addresses { get; set; }

    [JsonPropertyName("lines")]
    public object Lines { get; set; }

    [JsonPropertyName("vasCodes")]
    public object VasCodes { get; set; }
}


