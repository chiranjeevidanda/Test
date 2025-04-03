using MediatR;
using NEC.Fulf3PL.Application.Admin.Services;
using NEC.Fulf3PL.Application.Queries.Pagination;
using NEC.Fulf3PL.Core.Entities.Admin;
using NEC.Fulf3PL.Core.Common.Admin;
using NEC.Fulf3PL.Application.Admin.Common;
using NEC.Fulf3PL.Infrastructure.Persistence.Admin.Options;
using Microsoft.Extensions.Options;

namespace NEC.Fulf3PL.Application.Admin.Query;

public record OutboundTransactionDashboardQuery
    : IRequest<PaginationResponseModel<string>>;

public class OutboundTransactionDashboardQueryHandler
    : IRequestHandler<OutboundTransactionDashboardQuery, PaginationResponseModel<string>>
{
    private readonly IAdminOutboundRequestsQueryService _queryService;
    private readonly AdminOutboundCustomerOptions _outboundCustomerOptions;

    public OutboundTransactionDashboardQueryHandler(IAdminOutboundRequestsQueryService queryService, IOptions<AdminOutboundCustomerOptions> outboundCustomerOptions, IOptions<ExculdePlantOrderOptions> exculdePlantOrder)
    {
        _queryService = queryService;
        _outboundCustomerOptions = outboundCustomerOptions.Value;
    }

    public async Task<PaginationResponseModel<string>> Handle(OutboundTransactionDashboardQuery request, CancellationToken cancellationToken)
    {
        var validReturnOrderCustomer = GetCustomerForDocumentType(OutboundTransactionRequestType.ReturnOrder);
        var validPurchaseOrderCustomer = GetCustomerForDocumentType(OutboundTransactionRequestType.PurchaseOrder);
        var validCreateOrderCustomer = GetCustomerForDocumentType(OutboundTransactionRequestType.CreateOrder);
        var validProductMasterCustomer = GetCustomerForDocumentType(OutboundTransactionRequestType.ProductMaster);
        var docsList = await _queryService.GetItemsAsync(
            x => x.RequestInput != null && (
            (x.DocumentType == OutboundTransactionRequestType.ReturnOrder && validReturnOrderCustomer.Contains(x.Customer) 
            ) ||
            x.DocumentType == nameof(OutboundTransactionRequestType.PurchaseOrder) && validPurchaseOrderCustomer.Contains(x.Customer) ||
            (x.DocumentType == nameof(OutboundTransactionRequestType.CreateOrder) && validCreateOrderCustomer.Contains(x.Customer) 
            ) ||
            x.DocumentType == nameof(OutboundTransactionRequestType.ProductMaster) && validProductMasterCustomer.Contains(x.Customer)),
            x => new OutboundRequests
            {
                Status = x.Status,
                DocumentType = x.DocumentType
            }
        );

        var groupedList = docsList
            .GroupBy(x => x.DocumentType)
            .Select(g => new
            {
                RequestType = g.Key,
                TotalCount = g.Count(),
                InProgressCount = g.Count(x => x.Status == TransactionStatus.Inprogress)
            })
            .ToList();

        var resultList = groupedList
            .Select(g => $"{Helpers.ToOutboundDocumentType(g.RequestType)} - {g.InProgressCount} out of {g.TotalCount} processing").ToList();


        return new PaginationResponseModel<string>(resultList, resultList.Count());
    }

    private List<string> GetCustomerForDocumentType(string requestType) => requestType switch
    {
        OutboundTransactionRequestType.ReturnOrder => _outboundCustomerOptions.ReturnOrder,
        OutboundTransactionRequestType.PurchaseOrder => _outboundCustomerOptions.PurchaseOrder,
        OutboundTransactionRequestType.CreateOrder => _outboundCustomerOptions.CreateOrder,
        OutboundTransactionRequestType.ProductMaster => _outboundCustomerOptions.ProductMaster,
        _ => new List<string>()
    };

}