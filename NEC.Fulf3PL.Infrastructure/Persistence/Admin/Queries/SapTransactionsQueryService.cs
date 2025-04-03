using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Extensions.Options;
using NEC.Fulf3PL.Infrastructure.Persistence.CosmosDB.Options;
using NEC.Fulf3PL.Application.Admin.Dtos;
using NEC.Fulf3PL.Application.Admin.Services;
using NEC.Fulf3PL.Application.Extensions;
using NEC.Fulf3PL.Application.Queries.Pagination;
using NEC.Fulf3PL.Core.Entities.Admin;
using NEC.Fulf3PL.Infrastructure.Persistence.CosmosDB;
using NEC.Fulf3PL.Infrastructure.Persistence.CosmosDB.Extensions;
using System.Data;
using System.Linq.Expressions;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using NEC.Fulf3PL.Core.Common.Admin;
using NEC.Fulf3PL.Infrastructure.Persistence.Admin.Options;
using NEC.Fulf3PL.Application.Admin.Common;

namespace NEC.Fulf3PL.Infrastructure.Persistence.Admin.Queries;

public class SapTransactionsQueryService : GenericQueryService<SapTransactionLog>, ISapTransactionsQueryService
{
    private readonly AdminInboundCustomerOptions _inboundCustomerOptions;
    private readonly CustomerPlantOptions _provider;
    public SapTransactionsQueryService(CosmosClient cosmosClient, IOptions<CosmosDbConfigOptions> options, IOptions<AdminInboundCustomerOptions> inboundCustomerOptions,
        IOptions<ExculdePlantOrderOptions> exculdePlantOrder, IOptions<CustomerPlantOptions> provider)
        : base(cosmosClient, options.Value, options.Value.AdminSapTransactionLogContainer)
    {
        _inboundCustomerOptions = inboundCustomerOptions.Value;
        _provider = provider.Value;
    }

    public async Task<PaginationResponseModel<InboundTransactionsDto>> ListDocuments(InboundTransactionsListSearchFilterDto filterDto, DateTime defaultStartDate)
    {
        if (filterDto.DocumentType != null)
        {
            var matchedType = InboundRequestTypeMappings(filterDto.DocumentType);
            if (string.IsNullOrEmpty(matchedType))
            {
                return new PaginationResponseModel<InboundTransactionsDto>(new List<InboundTransactionsDto>(), 0);
            }
        }

        var filterExpressions = new List<Expression<Func<SapTransactionLog, bool>>>()
            .AddItemIfValueNotNull(filterDto.DateFrom, x => x.RequestPayload.Timestamp > (filterDto.DateFrom.Value.Date.AddDays(1).AddTicks(-1)))
            .AddItemIfValueNotNull(filterDto.DateTo, x => x.RequestPayload.Timestamp <= filterDto.DateTo);
             filterExpressions.Add(x => x.Provider == _provider.ApplicationProvider);
        filterExpressions = ApplyRquestTypeFilterExpression(filterExpressions, filterDto.DocumentType);

        filterExpressions = ApplyOrderNumberFilterExpression(filterExpressions, filterDto, filterDto.DocumentType);

        if (filterExpressions.Count == 0)
        {
            filterExpressions.Add(x => x.LoggedOn >= defaultStartDate);
        }

        IQueryable<SapTransactionLog> queryable = _container.GetItemLinqQueryable<SapTransactionLog>();

        queryable = queryable.ApplyFilterExpressions(filterExpressions);

        var total = await queryable.CountAsync();

        var sortExpression = GetSortExpression(filterDto.SortBy);
        queryable = filterDto.IsAscending() ? queryable.OrderBy(sortExpression) : queryable.OrderByDescending(sortExpression);

        IEnumerable<InboundTransactionsDto> mappedItems = await GetMappedItems(queryable);

        mappedItems = SortbyOrderType(filterDto, mappedItems);

        mappedItems = mappedItems.Skip(filterDto.Skip).Take(filterDto.Take).ToList();
        var response = new PaginationResponseModel<InboundTransactionsDto>(mappedItems, total);
        return response;
    }

    private static IEnumerable<InboundTransactionsDto> SortbyOrderType(InboundTransactionsListSearchFilterDto filterDto, IEnumerable<InboundTransactionsDto> mappedItems)
    {
        if (!string.IsNullOrEmpty(filterDto.SortBy) && filterDto.SortBy.ToUpper() == "ORDERTYPE")
        {
            mappedItems = filterDto.IsAscending()
            ? mappedItems.OrderBy(x => x.OrderType, StringComparer.OrdinalIgnoreCase).ToList()
            : mappedItems.OrderByDescending(x => x.OrderType, StringComparer.OrdinalIgnoreCase).ToList();
        }

        return mappedItems;
    }
    private async Task<IEnumerable<InboundTransactionsDto>> GetMappedItems(IQueryable<SapTransactionLog> queryable)
    {
        var query = queryable.Select(x => new
        {
            x.Id,
            x.RequestType,
            x.InboundRequestId,
            x.Provider,
            x.RequestPayload,
            x.SapInputRequest,
            x.SapResponse,
            x.LoggedOn,
            x.WebhookPayload,
            x.ErrorMessage,
            x.TransactionStatus,
            x.Customer
        });

        var items = await query.ToFeedIterator().ToListAsync();
        var mappedItems = items.Select(tq => new InboundTransactionsDto
        {
            Id = tq.Id,
            OrderNumber = tq.RequestPayload == null ? string.Empty : ExtractOrderNumber(JObject.FromObject(tq.RequestPayload), tq.RequestType),
            Customer = tq.Customer,
            OrderType = tq?.RequestType != null ? InboundRequestTypeMappings(tq?.RequestType) : string.Empty,
            CreatedDate = tq.RequestPayload.Timestamp.GetFormattedDate(),
            SapBapiInput = tq?.SapInputRequest ?? string.Empty,
            SapBapiResponse = tq?.SapResponse == null ? string.Empty : JsonConvert.SerializeObject(tq.SapResponse),
            SapConverterPayload = tq?.RequestPayload == null ? string.Empty : JsonConvert.SerializeObject(tq.RequestPayload),
            WebhookPayload = tq?.WebhookPayload == null ? string.Empty : JsonConvert.SerializeObject(tq.WebhookPayload),
            ErrorMessage = tq?.ErrorMessage ?? string.Empty,
            TransactionStatus = tq?.TransactionStatus ?? string.Empty,
            Provider = tq.Provider
        });
        return mappedItems;
    }

    public static Expression<Func<SapTransactionLog, object?>> GetSortExpression(string? sortBy)
    {
        sortBy = sortBy?.ToUpperInvariant();
        Expression<Func<SapTransactionLog, object?>> sortExpression = sortBy switch
        {
            "TRANSACTIONSTATUS" => (SapTransactionLog pc) => pc.TransactionStatus,
            "ID" => (SapTransactionLog pc) => pc.Id,
            "ORDERTYPE" => (SapTransactionLog pc) => pc.RequestType,
            "ORDERNUMBER" => (SapTransactionLog pc) => pc.RequestPayload.AdjustmentNumber,
            "LOGGEDON" => (SapTransactionLog pc) => pc.LoggedOn,
            "CREATEDDATE" => (SapTransactionLog pc) => pc.RequestPayload.Timestamp,
            "REQUESTTYPE" => (SapTransactionLog pc) => pc.RequestType,
            _ => (SapTransactionLog pc) => pc.LoggedOn
        };
        return sortExpression;
    }

    private List<Expression<Func<SapTransactionLog, bool>>> ApplyRquestTypeFilterExpression(List<Expression<Func<SapTransactionLog, bool>>> filterExpressions, string requestType)
    {
        if (string.IsNullOrEmpty(requestType))
        {
            AddFilterExpressionWhenNoRequestType(filterExpressions);
        }
        else
        {
            filterExpressions.Add(spt => spt.RequestType == requestType);

            var validCustomerforRequestType = GetCustomerForDocumentType(requestType);

            if (requestType == InboundTransactionRequestType.Inventory)
            {
                filterExpressions.Add(spt => validCustomerforRequestType.Count() == 0 ? true : validCustomerforRequestType.Contains(spt.RequestPayload.Plant));
            }
            else
            {
                filterExpressions.Add(spt => validCustomerforRequestType.Count() == 0 ? true : validCustomerforRequestType.Contains(spt.RequestPayload.VendorId));
            }


        }

        return filterExpressions;
    }

    private void AddFilterExpressionWhenNoRequestType(List<Expression<Func<SapTransactionLog, bool>>> filterExpressions)
    {
        var validCustomer = GetValidCustomers();

        filterExpressions.Add(x => x.RequestType == InboundTransactionRequestType.Inventory && validCustomer.InventoryCustomer.Contains(x.RequestPayload.Plant) ||
            (x.RequestType == nameof(InboundTransactionRequestType.GoodsIssued) && validCustomer.GoodsIssuedCustomer.Contains(x.RequestPayload.VendorId)) ||
            (x.RequestType == nameof(InboundTransactionRequestType.ReturnReceived) && validCustomer.ReturnReceivedCustomer.Contains(x.RequestPayload.VendorId)) ||
            x.RequestType == nameof(InboundTransactionRequestType.GoodsReceived) && validCustomer.GoodsReceivedCustomer.Contains(x.RequestPayload.VendorId));
    }


    private string? ExtractOrderNumber(JObject payload, string requestType)
    {
        if (string.Equals(requestType, InboundTransactionRequestType.Inventory, StringComparison.InvariantCultureIgnoreCase))
        {
            return payload["adjustmentNumber"]?.ToString();
        }
        else
        {
            return payload["orderNumber"]?.ToString();
        }
    }

    private string? ExtractCustomer(JObject payload, string customer)
    {
        return payload != null && payload["customer"] != null             
          ? payload["customer"]!.ToString()
          : string.Empty;
    }

    private List<Expression<Func<SapTransactionLog, bool>>> ApplyOrderNumberFilterExpression(List<Expression<Func<SapTransactionLog, bool>>> filterExpressions, InboundTransactionsListSearchFilterDto filterDto, string requestType)
    {
        if (string.IsNullOrEmpty(filterDto.OrderNumber))
            return filterExpressions;
        bool isInventoryRequest = requestType == SapTransactionRequestType.Inventory;
        if (filterDto.OrderNumber.EndsWith('*'))
        {
            string wildcardOrderNumber = filterDto.OrderNumber.TrimEnd('*');

            filterExpressions.Add(spt =>
      spt.RequestPayload.AdjustmentNumber.StartsWith(wildcardOrderNumber, StringComparison.OrdinalIgnoreCase) ||
      spt.RequestPayload.OrderNumber.StartsWith(wildcardOrderNumber, StringComparison.OrdinalIgnoreCase));
        }
        else
        {
            var orderNumbers = filterDto.OrderNumber.Replace(" ", ",").Split(",", StringSplitOptions.RemoveEmptyEntries);
            filterExpressions.Add(spt => orderNumbers.Contains(spt.RequestPayload.OrderNumber) || orderNumbers.Contains(spt.RequestPayload.AdjustmentNumber));
        }

        return filterExpressions;
    }

    private static string? InboundRequestTypeMappings(string? requestType) => requestType switch
    {
        nameof(InboundTransactionRequestType.GoodsReceived) => InboundTransactionRequestType.GoodsReceived,
        nameof(InboundTransactionRequestType.GoodsIssued) => InboundTransactionRequestType.GoodsIssued,
        nameof(InboundTransactionRequestType.ReturnReceived) => InboundTransactionRequestType.ReturnReceived,
        nameof(InboundTransactionRequestType.Inventory) => InboundTransactionRequestType.Inventory,

        _ => null
    };

    private List<string> GetCustomerForDocumentType(string requestType) => requestType switch
    {
        nameof(InboundTransactionRequestType.GoodsReceived) => _inboundCustomerOptions.GoodsReceived,
        nameof(InboundTransactionRequestType.GoodsIssued) => _inboundCustomerOptions.GoodsIssued,
        nameof(InboundTransactionRequestType.ReturnReceived) => _inboundCustomerOptions.ReturnReceived,
        nameof(InboundTransactionRequestType.Inventory) => _inboundCustomerOptions.Inventory,
        _ => new List<string>()
    };


    private ValidCustomerLists GetValidCustomers()
    {
        return new ValidCustomerLists
        {
            InventoryCustomer = GetCustomerForDocumentType(nameof(InboundTransactionRequestType.Inventory)),
            GoodsIssuedCustomer = GetCustomerForDocumentType(nameof(InboundTransactionRequestType.GoodsIssued)),
            GoodsReceivedCustomer = GetCustomerForDocumentType(nameof(InboundTransactionRequestType.GoodsReceived)),
            ReturnReceivedCustomer = GetCustomerForDocumentType(nameof(InboundTransactionRequestType.ReturnReceived))
        };
    }
}
