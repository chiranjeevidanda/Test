namespace NEC.Fulf3PL.Application.Admin.Dtos;

public class DocumentForRetriggerDto
{
    public string? DocumentType { get; set; }

    public string? RequestData { get; set; }

    public object? RequestInput { get; set; }
    public string? DocumentId { get; set; }
    public string? Customer { get; set; }
    public string? ProcessId { get; set; }
    public object? WebhookPayload { get; set; }
}
