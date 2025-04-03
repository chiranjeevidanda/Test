namespace NEC.Fulf3PL.Infrastructure.Persistence.Admin.Options;

public class AdminRetriggerServiceOptions
{
    public const string SectionName = "RetriggerServiceSettings";
    public string PostInboundRetriggerDocumentsHttpTriggerUrl { get; set; } = string.Empty;
    public string PostOutboundRetriggerDocumentsHttpTriggerUrl { get; set; } = string.Empty;
    public string PostCreateSkuHttpTriggerUrl { get; set; } = string.Empty;
}
