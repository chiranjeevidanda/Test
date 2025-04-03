namespace NEC.Fulf3PL.Application.Admin.Dtos;

public class RetriggerRequestDto
{
    public string? DocumentType { get; set; }

    public string? DocumentId { get; set; }

    public DateTime? DateFrom { get; set; }

    public DateTime? DateTo { get; set; }

    public DateTime? SingleDate { get; set; }

    public string? EventId { get; set; }

    public string? Payload { get; set; }

    public string? Customer { get; set; }

    public bool? IsSingleDate { get; set; }
}
