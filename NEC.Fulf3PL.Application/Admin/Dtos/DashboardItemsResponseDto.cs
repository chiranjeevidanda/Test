using NEC.Fulf3PL.Application.Common.Converters;
using System.Text.Json.Serialization;

namespace NEC.Fulf3PL.Application.Admin.Dtos;

public class DashboardItemsResponseDto
{
    public string Module { get; set; } = string.Empty;

    public string DocumentId { get; set; } = string.Empty;

    public string DocumentName { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    [JsonConverter(typeof(DateTimeGeneralFormatConverter))]
    public DateTime CreatedDate { get; set; }

    [JsonConverter(typeof(DateTimeGeneralFormatConverter))]
    public DateTime ModifiedDate { get; set; }
}
