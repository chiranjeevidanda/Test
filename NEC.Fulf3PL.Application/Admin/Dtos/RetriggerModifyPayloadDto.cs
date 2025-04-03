using NEC.Fulf3PL.Application.Inbound.Implementation.COMMON.Model;
using Newtonsoft.Json;

namespace NEC.Fulf3PL.Application.Admin.Dtos;

public class RetriggerModifyPayloadDto
{
    public object WebhookPayload { get; set; }

    public InboundConverterModel ConverterModel { get; set; }

    public ProcessConfiguration ProcessConfiguration { get; set; }
}
