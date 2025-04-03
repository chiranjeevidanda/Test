namespace NEC.Fulf3PL.Application.Admin.Dtos;

public class InboundModifyRequestPayloadDto
{
    public string? EventId { get; set; }

    public string? DocumentId { get; set; }

    public string? Customer { get; set; }

    public string? DocumentType { get; set; }

    public string? RequestInput { get; set; }

    public object? WebhookPayload { get; set; }
}