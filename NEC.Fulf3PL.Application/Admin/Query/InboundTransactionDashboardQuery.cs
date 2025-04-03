using MediatR;
using NEC.Fulf3PL.Application.Admin.Services;
using NEC.Fulf3PL.Application.Queries.Pagination;
using NEC.Fulf3PL.Core.Common.Admin;

namespace NEC.Fulf3PL.Application.Admin.Query;

public record InboundTransactionDashboardQuery
    : IRequest<PaginationResponseModel<InboundServicebusStatistics>>;
public record InboundServicebusStatistics(string requestType, string displayName, long pendingMessageCount);

public class InboundTransactionDashboardQueryHandler
    : IRequestHandler<InboundTransactionDashboardQuery, PaginationResponseModel<InboundServicebusStatistics>>
{
    private readonly IInboundServiceBusQueueService _inboundServiceBusQueueService;

    public InboundTransactionDashboardQueryHandler(
        IInboundServiceBusQueueService inboundServiceBusQueueService)
    {
        _inboundServiceBusQueueService = inboundServiceBusQueueService;
    }

    public async Task<PaginationResponseModel<InboundServicebusStatistics>> Handle(InboundTransactionDashboardQuery request, CancellationToken cancellationToken)
    {
        var servicebusQueuePendingMessage = await _inboundServiceBusQueueService.GetActiveMessageCountAsync();

        var messageDetails = servicebusQueuePendingMessage.Select(queue => new InboundServicebusStatistics(requestType: queue.Key, displayName: ToDocumentType(queue.Key), pendingMessageCount: queue.Value)).ToList();

        return new PaginationResponseModel<InboundServicebusStatistics>(messageDetails, messageDetails.Count);
    }

    private static string ToDocumentType(string functionName) => functionName switch
    {
        nameof(InboundTransactionRequestType.GoodsReceived) => "PO Receipt",
        nameof(InboundTransactionRequestType.GoodsIssued) => "Shipment",
        nameof(InboundTransactionRequestType.ReturnReceived) => "Return Receipt",
        nameof(InboundTransactionRequestType.Inventory) => "Inventory",
        _ => string.Empty
    };
}