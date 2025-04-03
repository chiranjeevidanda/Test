using NEC.Fulf3PL.Application.Common.Converters;
using System.Text.Json.Serialization;

namespace NEC.Fulf3PL.Application.Admin.Dtos;

public class OutboundTransactionsDto
{
    [JsonPropertyName("createdDate")]
    [JsonConverter(typeof(DateTimeGeneralFormatConverter))]
    public DateTime CreatedDate { get; set; }

    [JsonPropertyName("eventId")]
    public string? EventId { get; set; }

    [JsonPropertyName("productCode")]
    public string? ProductCode { get; set; } = string.Empty;
    
    [JsonPropertyName("ktnResponse")]
    public string? KTNResponse { get; set; } = string.Empty;

    [JsonPropertyName("orderType")]
    public string? OrderType { get; set; } = string.Empty;
    
    [JsonPropertyName("pONumber")]
    public string? PONumber { get; set; } = string.Empty;
}
