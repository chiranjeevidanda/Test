using Ardalis.GuardClauses;
using MediatR;
using Microsoft.Extensions.Options;
using NEC.Fulf3PL.Application.Admin.Common;
using NEC.Fulf3PL.Application.Admin.Options;
using NEC.Fulf3PL.Application.Admin.Services;
using NEC.Fulf3PL.Application.Admin.Services.ExcelGenerateService;
using NEC.Fulf3PL.Application.Common.EmailSenderService;
using NEC.Fulf3PL.Application.Extensions;
using NEC.Fulf3PL.Core.Common.Admin;
using NEC.Fulf3PL.Core.DTO.Admin;
using NEC.Fulf3PL.Core.Entities.Admin;
using Newtonsoft.Json.Linq;
namespace NEC.Fulf3PL.Application.Admin.Command;

public record SendMailForFailedInboundDocumentReportCommand(DateTime fetchDateFrom) : IRequest<bool>;

public class SendMailForFailedInboundDocumentReportCommandHandler : IRequestHandler<SendMailForFailedInboundDocumentReportCommand, bool>
{
    private readonly IApiReportSpreadsheetService _apiReportSpreadsheetService;
    private readonly IEmailService _emailService;
    private readonly ISapTransactionsQueryService _sapTransactionsQuery;
    private readonly EmailNotificationOptions _options;

    public SendMailForFailedInboundDocumentReportCommandHandler(IApiReportSpreadsheetService apiReportSpreadsheetService, IEmailService emailService, ISapTransactionsQueryService sapTransactionsQuery, IOptions<EmailNotificationOptions> options)
    {
        _apiReportSpreadsheetService = apiReportSpreadsheetService;
        _sapTransactionsQuery = sapTransactionsQuery;
        _emailService = emailService;
        _options = options.Value;
    }


    public async Task<bool> Handle(SendMailForFailedInboundDocumentReportCommand request, CancellationToken cancellationToken)
    {
        Guard.Against.NullOrEmpty(_options.Environment);
        Guard.Against.NullOrEmpty(_options.Provider);

        var documentsList = await GetFailedDocumentsAsync(request.fetchDateFrom);

        if (!documentsList.Any())
        {
            return true;
        }

        await GenerateReport(documentsList);

        return true;
    }

    private async Task GenerateReport(IEnumerable<SapTransactionLog> documentsList)
    {
        var groupedDocs = documentsList.GroupBy(x => x.RequestType).ToList();
        foreach (var group in groupedDocs)
        {
            var transaction = group.Select(transaction => transaction);

            switch (group.Key)
            {
                case nameof(InboundTransactionRequestType.Inventory):
                    var adjustmentReports = group.Select(CreateAdjustmentReport).ToList();
                    await SendInventoryAdjustmentReportNotification(adjustmentReports);
                    break;
                case nameof(InboundTransactionRequestType.ReturnReceived):
                    var returnReceiptReports = CreateReturnReceiptReport(transaction);
                    await SendReturnReceivedReportNotification(returnReceiptReports);
                    break;
                case nameof(InboundTransactionRequestType.GoodsReceived):
                    var poReceiptReports = CreatePoReceiptReport(transaction);
                    await SendGoodsReceivedReportNotification(poReceiptReports);
                    break;
            }
        }
    }

    private async Task<IEnumerable<SapTransactionLog>> GetFailedDocumentsAsync(DateTime fromDate)
    {
        return await _sapTransactionsQuery.GetItemsAsync(
             record => record.LoggedOn >= fromDate && record.Status == false && record.Provider == _options.Provider &&
                       (record.RequestType == nameof(InboundTransactionRequestType.Inventory) ||
                        record.RequestType == nameof(InboundTransactionRequestType.ReturnReceived) ||
                        record.RequestType == nameof(InboundTransactionRequestType.GoodsReceived)),
             x => new SapTransactionLog
             {
                 Id = x.Id,
                 RequestType = x.RequestType,
                 RequestPayload = x.RequestPayload,
                 ErrorMessage = x.ErrorMessage,
                 LoggedOn = x.LoggedOn,
             });
    }

    private async Task SendInventoryAdjustmentReportNotification(List<InventoryAdjustmentReportDto> adjustmentReports)
    {
        var generatedFileContent = _apiReportSpreadsheetService.GenerateExcel(adjustmentReports);

        var subject = ConstantsHelpers.InboubdApiErrorReportSubject(InboundTransactionRequestType.Inventory, _options.Environment, _options.Provider);

        var fileName = ConstantsHelpers.InboubdApiErrorReportAttachmentName(InboundTransactionRequestType.Inventory, _options.Provider);

        var recipientEmails = GetRecipients(InboundTransactionRequestType.Inventory, _options);

        if (recipientEmails?.Count > 0)
        {
            await _emailService.SendMail(generatedFileContent, recipientEmails, subject, fileName);
        }
    }

    private async Task SendReturnReceivedReportNotification(List<ReturnReceiptReportDto> adjustmentReports)
    {
        var generatedFileContent = _apiReportSpreadsheetService.GenerateExcel(adjustmentReports, _options.Provider);

        var subject = ConstantsHelpers.InboubdApiErrorReportSubject(InboundTransactionRequestType.ReturnReceived, _options.Environment, _options.Provider);

        var fileName = ConstantsHelpers.InboubdApiErrorReportAttachmentName(InboundTransactionRequestType.ReturnReceived, _options.Provider);

        var recipientEmails = GetRecipients(InboundTransactionRequestType.ReturnReceived, _options);

        if (recipientEmails?.Count > 0)
        {
            await _emailService.SendMail(generatedFileContent, recipientEmails, subject, fileName);
        }
    }

    private async Task SendGoodsReceivedReportNotification(List<PoReceiptReportDto> adjustmentReports)
    {
        var generatedFileContent = _apiReportSpreadsheetService.GenerateExcel(adjustmentReports, _options.Provider);

        var subject = ConstantsHelpers.InboubdApiErrorReportSubject(InboundTransactionRequestType.GoodsReceived, _options.Environment, _options.Provider);

        var fileName = ConstantsHelpers.InboubdApiErrorReportAttachmentName(InboundTransactionRequestType.GoodsReceived, _options.Provider);

        var recipientEmails = GetRecipients(InboundTransactionRequestType.GoodsReceived, _options);
        
        if (recipientEmails?.Count > 0)
        {
            await _emailService.SendMail(generatedFileContent, recipientEmails, subject, fileName);
        }
    }

    private List<ReturnReceiptReportDto> CreateReturnReceiptReport(IEnumerable<SapTransactionLog> doc)
    {
        var report = new List<ReturnReceiptReportDto>();
        foreach (SapTransactionLog log in doc)
        {
            if (log?.RequestPayload?.Entries?.Count() == 0)
            {
                ReturnReceiptReportDto poReceipt = GetReturnReceiptReportData(log, null);
                report.Add(poReceipt);
            }
            else
            {
                foreach (var entry in log?.RequestPayload?.Entries)
                {
                    var entryDetails = JObject.FromObject(entry);
                    ReturnReceiptReportDto poReceipt = GetReturnReceiptReportData(log, entryDetails);

                    report.Add(poReceipt);
                }
            }
        }

        return report;
    }

    private List<PoReceiptReportDto> CreatePoReceiptReport(IEnumerable<SapTransactionLog> doc)
    {
        var report = new List<PoReceiptReportDto>();
        foreach (SapTransactionLog log in doc)
        {
            if (log?.RequestPayload?.Entries?.Count() == 0)
            {
                PoReceiptReportDto poReceipt = GetPoReceiptReportData(log, null);
                report.Add(poReceipt);
            }
            else
            {
                foreach (var entry in log?.RequestPayload?.Entries)
                {
                    var entryDetails = JObject.FromObject(entry);
                    PoReceiptReportDto poReceipt = GetPoReceiptReportData(log, entryDetails);
                    report.Add(poReceipt);
                }
            }
        }

        return report;
    }

    private ReturnReceiptReportDto GetReturnReceiptReportData(SapTransactionLog log, JObject? entryDetails)
    {
        var poReceipt = new ReturnReceiptReportDto();

        poReceipt.EventId = log.Id;
        poReceipt.EventTimestamp = log.RequestPayload?.Timestamp.GetFormattedDate();
        poReceipt.SAPReturnMessage = log.ErrorMessage;
        poReceipt.KTNUniqueIdentifier = log.RequestPayload?.RequestId;
        poReceipt.ReceiptDate = log.RequestPayload?.ReceiptDate.GetFormattedDate();
        poReceipt.NecSapPlant = log.RequestPayload?.VendorId;
        poReceipt.NecPurchaseOrderNumber = log.RequestPayload?.OrderNumber;
        poReceipt.DeliveryItem = Helpers.GetLinNo(entryDetails);
        poReceipt.PoLineItem = Helpers.GetLastFiveCharacters(entryDetails);
        poReceipt.PoScheduleLine = Helpers.GetFirstFourCharacters(entryDetails);
        poReceipt.QtyReceived = entryDetails?["quantity"] == null ? string.Empty : entryDetails["quantity"]?.ToString();
        poReceipt.UPC = entryDetails?["productCode"] == null ? string.Empty : entryDetails["productCode"]?.ToString();
        poReceipt.MID = entryDetails?["materilId"] == null ? string.Empty : entryDetails["materilId"]?.ToString();

        return poReceipt;
    }

    private PoReceiptReportDto GetPoReceiptReportData(SapTransactionLog log, JObject? entryDetails)
    {
        var poReceipt = new PoReceiptReportDto();

        poReceipt.EventId = log.Id;
        poReceipt.EventTimestamp = log.RequestPayload?.Timestamp.GetFormattedDate();
        poReceipt.SAPReturnMessage = log.ErrorMessage;
        poReceipt.KTNUniqueIdentifier = log.RequestPayload?.DeliveryNumber;
        poReceipt.ReceiptDate = log.RequestPayload?.ReceiptDate.GetFormattedDate();
        poReceipt.NecSapPlant = log.RequestPayload?.VendorId;
        poReceipt.NecPurchaseOrderNumber = log.RequestPayload?.OrderNumber;
        poReceipt.LINNO = Helpers.GetLinNo(entryDetails);
        poReceipt.PoLineItem = Helpers.GetLastFiveCharacters(entryDetails);
        poReceipt.PoScheduleLine = Helpers.GetFirstFourCharacters(entryDetails);
        poReceipt.QtyReceived = entryDetails?["quantity"] == null ? string.Empty : entryDetails["quantity"]?.ToString();
        poReceipt.UPC = entryDetails?["productCode"] == null ? string.Empty : entryDetails["productCode"]?.ToString();
        poReceipt.MID = entryDetails?["materilId"] == null ? string.Empty : entryDetails["materilId"]?.ToString();
        return poReceipt;
    }

    private InventoryAdjustmentReportDto CreateAdjustmentReport(SapTransactionLog doc)
    {
        var transaction = new InventoryAdjustmentReportDto
        {
            EventId = doc.Id,
            ReceivedTimestamp = doc.RequestPayload?.Timestamp.GetFormattedDate(),
            NecSapPlant = doc.RequestPayload?.Plant,
            AdjustmentNumber = doc.RequestPayload?.AdjustmentNumber,
            AdjustmentDate = doc.RequestPayload?.AdjustmentDate,
            Quantity = $"{ConstantsHelpers.GetDirection(doc.RequestPayload?.Direction)} {doc.RequestPayload?.Change}",
            UPC = doc.RequestPayload?.ProductCode,
            AdjustmentDirection = doc.RequestPayload?.Direction,
            AdjustmentReasonText = doc.RequestPayload?.AdjustmentReasonText,
            ErrorReason = doc.ErrorMessage
        };

        return transaction;
    }

    private List<string> GetRecipients(string documentType, EmailNotificationOptions options)
    {
        var emailsList = new List<string>();

        if (options.RecipientEmailList != null)
        {
            emailsList.AddRange(options.RecipientEmailList);
        }

        string? emails = documentType switch
        {
            InboundTransactionRequestType.Inventory => options.Inventory,
            InboundTransactionRequestType.ReturnReceived => options.ReturnReceived,
            InboundTransactionRequestType.GoodsReceived => options.GoodsReceived,
            _ => null
        };

        if (!string.IsNullOrEmpty(emails))
        {
            var emailList = emails.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList();
            emailsList.AddRange(emailList);
        }

        return emailsList;
    }
}
