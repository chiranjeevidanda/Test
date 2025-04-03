using NEC.Fulf3PL.Application.Admin.Dtos;
using NEC.Fulf3PL.Core.Entities.Admin;
namespace NEC.Fulf3PL.Application.Admin.Services
{
    public interface IOutboundDocumentService
    {
        Task<List<EmailNotificationMessageDto>?> GetOutboundForEmailNotificationAsync(string documentId, string documentType, string customer, string provider);

        OutboundRequests? GetFailedDocument(List<EmailNotificationMessageDto> outboundRequests, string documentId, string documentType, string customer);
    }
}
