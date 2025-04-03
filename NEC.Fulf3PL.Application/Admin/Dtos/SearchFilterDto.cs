namespace NEC.Fulf3PL.Application.Admin.Dtos;

public class SearchFilterDto
{
    public string? DocumentType { get; set; }

    public DateTime? DateFrom { get; set; }

    public DateTime? DateTo { get; set; }

    public string? DocumentId { get; set; }

    public string? Status { get; set; }
}
