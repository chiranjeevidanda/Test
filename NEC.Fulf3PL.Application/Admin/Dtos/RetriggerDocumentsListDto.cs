namespace NEC.Fulf3PL.Application.Admin.Dtos;

public class RetriggerDocumentsListDto
{
    public  List<RetriggerEventDetail> RetriggerEventDetails { get; set; } = new List<RetriggerEventDetail>();

    public string? RequestType { get; set; }

    public string? EventId { get; set; }

    public string? RetriggerPayload { get; set; }

    public string? SKUId { get; set; }
}
