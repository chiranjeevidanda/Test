namespace NEC.Fulf3PL.Application.Admin.Dtos;

public class ProductMasterModifyPayloadDto
{
    public string? EventId { get; set; }

    public string? DocumentId { get; set; }

    public string? Customer { get; set; }


    public string? DocumentType { get; set; }

    public object? RequestInput { get; set; }

    public string? RequestData { get; set; }
}