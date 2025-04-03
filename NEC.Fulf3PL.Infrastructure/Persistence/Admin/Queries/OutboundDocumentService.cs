
using NEC.Fulf3PL.Application.Admin.Dtos;
using NEC.Fulf3PL.Application.Admin.Services;
using NEC.Fulf3PL.Core.Common.Admin;
using NEC.Fulf3PL.Core.Entities;
using NEC.Fulf3PL.Core.Entities.Admin;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NEC.Fulf3PL.Infrastructure.Persistence.Admin.Queries
{
    public class OutboundDocumentService : IOutboundDocumentService
    {
        private readonly IAdminOutboundRequestsQueryService _queryService;

        public OutboundDocumentService(IAdminOutboundRequestsQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<List<EmailNotificationMessageDto>?> GetOutboundForEmailNotificationAsync(string documentId, string documentType, string customer, string provider)
        {
            var transactionsLogs = await _queryService.GetItemsAsync(x => x.DocumentId == documentId && x.Customer == customer && x.LoggedOn >= DateTime.UtcNow.AddMinutes(-15) && x.Provider == provider);

            var transactions = transactionsLogs
                   .Select(log => new EmailNotificationMessageDto
                   {
                       ErrorMessage = log.ErrorMessage,
                       EventId = log.Id,
                       LoggedOn = log.LoggedOn,
                       RequestPayload = log.RequestData,
                       Status = log.Status,
                   })
                   .OrderByDescending(transaction => transaction.LoggedOn);

            return transactions.ToList();
        }

        public OutboundRequests? GetFailedDocument(List<EmailNotificationMessageDto> outboundRequests, string documentId, string documentType, string customer)
        {
            return outboundRequests?.Where(x => x.Status == TransactionStatus.Failed).Select(x => ExtractFailedDocument(x, documentId, documentType, customer)).FirstOrDefault();
        }

        private OutboundRequests ExtractFailedDocument(EmailNotificationMessageDto x, string documentId, string documentType, string customer)
        {
            var document = new OutboundRequests();

            if (x.ErrorMessage != null)
            {
                if (!IsJson(x.ErrorMessage))
                {
                    document.ErrorMessage = x.ErrorMessage;
                }
                else
                {
                    var parsedResponse = JObject.Parse(x.ErrorMessage);
                    var errorMessage = parsedResponse["error"]?["message"]?.ToString();

                    if (string.IsNullOrEmpty(errorMessage))
                    {
                        errorMessage = parsedResponse["message"]?.ToString();
                    }

                    document.ErrorMessage = errorMessage;
                }
            }

            document.RequestData = x.RequestPayload;
            document.Id = x.EventId;
            document.ModifiedDate = x.LoggedOn;
            document.Customer = customer;
            document.DocumentType = documentType;
            document.DocumentId = documentId;

            return document;
        }

        private static bool IsJson(string input)
        {
            input = input.Trim();
            return input.StartsWith("{") && input.EndsWith("}") ||
                   input.StartsWith("[") && input.EndsWith("]");
        }
    }
}
