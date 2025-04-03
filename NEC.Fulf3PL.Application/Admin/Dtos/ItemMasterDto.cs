using System.Text.Json.Serialization;

namespace NEC.Fulf3PL.Application.Admin.Dtos;

public class ItemMasterDto
{
    [JsonPropertyName("requestId")]
    public string? RequestId { get; set; } = string.Empty;
      
    [JsonPropertyName("skuId")]
    public string? SKUId { get; set; } = string.Empty;
    
    [JsonPropertyName("productDescription")]
    public string? ProductDescription { get; set; } = string.Empty;
}
