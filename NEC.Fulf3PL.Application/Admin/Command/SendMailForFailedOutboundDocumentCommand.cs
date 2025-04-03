using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NEC.Fulf3PL.Application.Admin.Common;
using NEC.Fulf3PL.Application.Admin.Options;
using NEC.Fulf3PL.Application.Admin.Services;
using NEC.Fulf3PL.Application.Common.Converters;
using NEC.Fulf3PL.Application.Common.EmailSenderService;
using NEC.Fulf3PL.Core.Common;
using NEC.Fulf3PL.Core.Entities.Admin;
using System.Text.RegularExpressions;
using NEC.Fulf3PL.Core.Common.Admin;

namespace NEC.Fulf3PL.Application.Admin.Command;

public record SendMailForFailedOutboundDocumentCommand(OutboundResponseDto entity) : IRequest<bool>;

public class SendMailForFailedOutboundDocumentCommandHandler : IRequestHandler<SendMailForFailedOutboundDocumentCommand, bool>
{

    private readonly IEmailService _emailService;
    private readonly IOutboundDocumentService _outboundDocumentService;
    private readonly ILogger<SendMailForFailedOutboundDocumentCommandHandler> _logger;
    private readonly EmailNotificationOptions _options;

    public SendMailForFailedOutboundDocumentCommandHandler(IEmailService emailService, IOptions<EmailNotificationOptions> options,
        IOutboundDocumentService outboundDocumentService, ILogger<SendMailForFailedOutboundDocumentCommandHandler> logger)
    {
        _outboundDocumentService = outboundDocumentService;
        _emailService = emailService;
        _logger = logger;
        _options = options.Value;
    }

    public async Task<bool> Handle(SendMailForFailedOutboundDocumentCommand command, CancellationToken cancellationToken)
    {
        var documentId = command.entity.DocumentId;
        var documentType = command.entity.DocumentType;
        var customer = command.entity.Customer;

        var documents = await _outboundDocumentService.GetOutboundForEmailNotificationAsync(documentId, documentType, customer, _options.Provider);

        if (documents?.Count <= 0 || documents?.Exists(x => x.Status == TransactionStatus.Success) == true)
        {
            return true;
        }
        var recipientEmails = GetRecipients(documentType, _options);

        var failedDocument = _outboundDocumentService.GetFailedDocument(documents, documentId, documentType, customer);
        if (failedDocument != null && recipientEmails?.Count > 0)
        {
            try
            {
                var subject = Helpers.OutboundApiErrorReportSubject(failedDocument.DocumentType, _options.Environment, _options.Provider);
                var messageContent = $"Date of Failure : {DateTimeGeneralFormatConverter.GetFormattedDate(failedDocument.ModifiedDate)}" +
                    $"\nCustomer : {failedDocument.Customer}," +
                    $"\nDocument ID : {failedDocument.DocumentId}," +
                    $"\n\nPayload : {failedDocument.RequestData}," +
                    $"\n\nError Reason : {failedDocument.ErrorMessage}";


                await _emailService.SendMail(subject, recipientEmails, messageContent);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while sending email for failed document processing.");
            }
        }

        return true;
    }

    private List<string> GetRecipients(string documentType, EmailNotificationOptions options)
    {
        var emailsList = new List<string>();

        if (options.RecipientEmailList?.Any() == true)
        {
            emailsList.AddRange(options.RecipientEmailList);
        }

        var emails = documentType switch
        {
            OutboundTransactionRequestType.ProductMaster => options.ProductMaster,
            OutboundTransactionRequestType.PurchaseOrder => options.PurchaseOrder,
            OutboundTransactionRequestType.CreateOrder => options.CreateOrder,
            OutboundTransactionRequestType.ReturnOrder => options.ReturnOrder,
            _ => null
        };

        if (!string.IsNullOrEmpty(emails))
        {
            emailsList.AddRange(emails.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries));
        }

        return emailsList.Distinct(StringComparer.OrdinalIgnoreCase).ToList();
    }
}
