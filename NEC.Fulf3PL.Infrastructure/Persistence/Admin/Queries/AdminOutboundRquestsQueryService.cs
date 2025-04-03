using LogicExtensions;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Extensions.Options;
using NEC.Fulf3PL.Infrastructure.Persistence.CosmosDB.Options;
using NEC.Fulf3PL.Application.Admin.Dtos;
using NEC.Fulf3PL.Application.Admin.Services;
using NEC.Fulf3PL.Application.Extensions;
using NEC.Fulf3PL.Application.Outbound.Implementation.COMMON.Helpers;
using NEC.Fulf3PL.Application.Queries.Pagination;
using NEC.Fulf3PL.Core.Common.Admin;
using NEC.Fulf3PL.Core.Entities.Admin;
using NEC.Fulf3PL.Infrastructure.Persistence.Admin.Options;
using NEC.Fulf3PL.Infrastructure.Persistence.CosmosDB;
using NEC.Fulf3PL.Infrastructure.Persistence.CosmosDB.Extensions;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Linq.Expressions;
using Newtonsoft.Json;
using NEC.Fulf3PL.Core.Common;

namespace NEC.Fulf3PL.Infrastructure.Persistence.Admin.Queries;

public class AdminOutboundRquestsQueryService : GenericQueryService<OutboundRequests>, IAdminOutboundRequestsQueryService
{
    private readonly AdminOutboundCustomerOptions _outboundCustomerOptions;
    private const string ISO_TIMESTAMP_FORMAT = "yyyy-MM-ddTHH:mm:ssZ";
    private readonly CustomerPlantOptions _provider;
    public AdminOutboundRquestsQueryService(CosmosClient cosmosClient, IOptions<CosmosDbConfigOptions> options, IOptions<AdminOutboundCustomerOptions> outboundCustomerOptions, IOptions<ExculdePlantOrderOptions> exculdePlantOrder, IOptions<CustomerPlantOptions> provider)
        : base(cosmosClient, options.Value, options.Value.AdminOutboundRequestsContainer)
    {
        _outboundCustomerOptions = outboundCustomerOptions.Value;
        _provider = provider.Value;
    }

    public async Task<PaginationResponseModel<OutboundResponseDto>> ListDocuments(OutboundTransactionsListSearchFilterDto filterDto, DateTime defaultStartDate)
    {
        if (filterDto.DocumentType != null)
        {
            var matchedType = GetOutboundRequestTypes(filterDto.DocumentType);
            if (string.IsNullOrEmpty(matchedType))
            {
                return new PaginationResponseModel<OutboundResponseDto>(new List<OutboundResponseDto>(), 0);
            }
        }
        var filterExpressions = new List<Expression<Func<OutboundRequests, bool>>>()
            .AddItemIfValueNotNull(filterDto.DateFrom, x => x.ModifiedDate > filterDto.DateFrom.Value.Date.AddDays(1).AddTicks(-1))
            .AddItemIfValueNotNull(filterDto.DateTo, x => x.ModifiedDate < filterDto.DateTo);
        filterExpressions.Add(x => x.Provider == _provider.ApplicationProvider);
        filterExpressions.Add(x => x.RequestData != null);

        filterExpressions.Add(x => x.Status == TransactionStatus.Success || x.Status == TransactionStatus.Failed || x.Status == TransactionStatus.Inprogress);

        filterExpressions = ApplyRequestTypeFilterExpression(filterExpressions, filterDto.DocumentType);
        filterExpressions = ApplyOrderNumberFilterExpression(filterExpressions, filterDto);
        IQueryable<OutboundRequests> transactionQueryable = _container.GetItemLinqQueryable<OutboundRequests>();
        transactionQueryable = transactionQueryable.ApplyFilterExpressions(filterExpressions);

        var total = await transactionQueryable.CountAsync();

        var sortExpression = GetSortExpression(filterDto.SortBy);

        transactionQueryable = filterDto.IsAscending() ? transactionQueryable.OrderBy(sortExpression) : transactionQueryable.OrderByDescending(sortExpression);
        var query = transactionQueryable.Select(x => new
        {
            x.Id,
            x.DocumentId,
            DocumentType = x.DocumentType.ToString(),
            x.ModifiedDate,
            x.Provider,
            Status = x.Status.ToString(),
            x.RequestInput,
            x.RequestData,
            x.Customer,
            x.ErrorMessage,
            x.SystemId,
        });

        var items = await query.ToFeedIterator().ToListAsync();

        var mappedItems = items.Select(x => new OutboundResponseDto
        {
            EventId = x.Id ?? string.Empty,
            DocumentType = ToDocumentType(x.DocumentType) ?? string.Empty,
            ModifiedDate = x.ModifiedDate.ToString(ISO_TIMESTAMP_FORMAT),
            Status = x.Status ?? string.Empty,
            DocumentId = x.DocumentId ?? string.Empty,
            RequestData = x.RequestData,
            ContainerNumber = x.RequestData == null ? string.Empty : ExtractContainerNumber(JObject.Parse(x.RequestData), x.DocumentType),
            RequestInput = x.RequestInput != null ? ParseRequestInput(JObject.FromObject(x.RequestInput)) : null,
            Customer = x.Customer,
            ErrorMessage = x.ErrorMessage != null ? GetErrorMessages(x.ErrorMessage) : string.Empty,
            SystemId = x.SystemId != null ? x.SystemId : string.Empty,
            Provider = x.Provider,
        });

        mappedItems = SortbyDeliverytype(filterDto, mappedItems);
        mappedItems = mappedItems.Skip(filterDto.Skip).Take(filterDto.Take).ToList();
        var response = new PaginationResponseModel<OutboundResponseDto>(mappedItems, total);
        return response;
    }
    private static IEnumerable<OutboundResponseDto> SortbyDeliverytype(OutboundTransactionsListSearchFilterDto filterDto, IEnumerable<OutboundResponseDto> mappedItems)
    {
        if (!string.IsNullOrEmpty(filterDto.SortBy) && filterDto.SortBy.ToUpper() == "DOCUMENTTYPE")
        {
            mappedItems = filterDto.IsAscending()
            ? mappedItems.OrderBy(x => x.DocumentType, StringComparer.OrdinalIgnoreCase).ToList()
            : mappedItems.OrderByDescending(x => x.DocumentType, StringComparer.OrdinalIgnoreCase).ToList();
        }

        return mappedItems;
    }
    private string? GetErrorMessages(string failureError)
    {
        if (string.IsNullOrEmpty(failureError))
        {
            return string.Empty;
        }

        try
        {
            JToken parsedResponse = JToken.Parse(failureError);

            if (parsedResponse.Type == JTokenType.Object)
            {
                return GetErrorMessageFromObject(parsedResponse);
            }

            if (parsedResponse.Type == JTokenType.Array)
            {
                return GetErrorMessageFromArray(failureError);
            }
        }
        catch
        {
            return failureError;
        }

        return failureError;
    }

    private string? GetErrorMessageFromObject(JToken parsedResponse)
    {
        string? errorMessage = parsedResponse["error"]?["message"]?.ToString()
                               ?? parsedResponse["message"]?.ToString();
        return errorMessage;
    }

    private string? GetErrorMessageFromArray(string failureError)
    {
        var exceptions = JsonConvert.DeserializeObject<List<ExecuteMessage>>(failureError);
        var jValue = JToken.Parse(exceptions[0].Description);

        string? errorMessage = jValue["error"]?["message"]?.ToString()
                               ?? jValue["message"]?.ToString();
        return errorMessage;
    }

    private static DeliveryDataModel ParseRequestInput(JObject requestInput)
    {
        return requestInput.ToObject<DeliveryDataModel>();
    }

    public static Expression<Func<OutboundRequests, object?>> GetSortExpression(string? sortBy)
    {
        sortBy = sortBy?.ToUpperInvariant();
        Expression<Func<OutboundRequests, object?>> sortExpression = sortBy switch
        {
            "MODIFIEDDATE" => (OutboundRequests pc) => pc.ModifiedDate,
            "DOCUMENTTYPE" => (OutboundRequests pc) => pc.DocumentType,
            "EVENTID" => (OutboundRequests pc) => pc.Id,
            "CUSTOMER" => (OutboundRequests pc) => pc.Customer,
            "DOCUMENTID" => (OutboundRequests pc) => pc.DocumentId,
            "STATUS" => (OutboundRequests pc) => pc.Status,
            _ => (OutboundRequests pc) => pc.ModifiedDate
        };
        return sortExpression;
    }
    private string ExtractContainerNumber(JObject payload, string requestType)
    {
        if (!string.Equals(requestType, OutboundTransactionRequestType.PurchaseOrder, StringComparison.OrdinalIgnoreCase))
            return string.Empty;
        if (payload["lines"] is JArray lines && lines.FirstOrDefault()?["containerNumber"]?.ToString() is string containerNumber)
        {
            return containerNumber;
        }
        return string.Empty;
    }
    private List<Expression<Func<OutboundRequests, bool>>> ApplyRequestTypeFilterExpression(List<Expression<Func<OutboundRequests, bool>>> filterExpressions, string requestType)
    {
        if (string.IsNullOrEmpty(requestType))
        {
            AddFilterExpressionWhenNoRequestType(filterExpressions);
        }
        else
        {
            filterExpressions.Add(x => x.DocumentType == requestType.ToString());

            var validCustomerforRequestType = GetCustomerForDocumentType(requestType);

            filterExpressions.Add(spt => validCustomerforRequestType.Count() == 0 ? true : validCustomerforRequestType.Contains(spt.Customer));

        }

        return filterExpressions;
    }
    private void AddFilterExpressionWhenNoRequestType(List<Expression<Func<OutboundRequests, bool>>> filterExpressions)
    {
        var validCustomer = GetValidCustomers();

        filterExpressions.Add(x => (x.DocumentType == OutboundTransactionRequestType.ReturnOrder && validCustomer.ReturnOrderCustomer.Contains(x.Customer)) ||
      (x.DocumentType == nameof(OutboundTransactionRequestType.PurchaseOrder) && validCustomer.PurchaseOrderCustomer.Contains(x.Customer)) ||
      (x.DocumentType == nameof(OutboundTransactionRequestType.CreateOrder) && validCustomer.CreateOrderCustomer.Contains(x.Customer)) ||
      (x.DocumentType == nameof(OutboundTransactionRequestType.ProductMaster) && validCustomer.ProductMasterCustomer.Contains(x.Customer)));

    }

    private static bool IsAlphanumericStart(string documentId)
    {
        char firstChar = documentId[0];
        return (firstChar >= '0' && firstChar <= '9') || // Check if digit
               (firstChar >= 'A' && firstChar <= 'Z') || // Check if uppercase letter
               (firstChar >= 'a' && firstChar <= 'z');   // Check if lowercase letter
    }

    private static string? GetOutboundRequestTypes(string requestType) => requestType is
                  OutboundTransactionRequestType.ReturnOrder or OutboundTransactionRequestType.PurchaseOrder or
                  OutboundTransactionRequestType.CreateOrder or OutboundTransactionRequestType.ProductMaster ? requestType : null;

    private List<Expression<Func<OutboundRequests, bool>>> ApplyOrderNumberFilterExpression(List<Expression<Func<OutboundRequests, bool>>> filterExpressions, OutboundTransactionsListSearchFilterDto filterDto)
    {
        if (!string.IsNullOrEmpty(filterDto.OrderNumber))
        {

            if (filterDto.OrderNumber.EndsWith('*'))
            {
                string wildcardOrderNumbers = filterDto.OrderNumber.TrimEnd('*');
                filterExpressions.Add(x => x.DocumentId.StartsWith(wildcardOrderNumbers, StringComparison.OrdinalIgnoreCase));
            }
            else
            {

                string[] orderNumbers = filterDto.OrderNumber.Replace(" ", ",").Split(",", StringSplitOptions.RemoveEmptyEntries);
                filterExpressions.Add(x => orderNumbers.Contains(x.DocumentId));
            }
        }

        return filterExpressions;
    }
    public List<string> GetCustomerForDocumentType(string requestType) => requestType switch
    {
        OutboundTransactionRequestType.ReturnOrder => _outboundCustomerOptions.ReturnOrder,
        OutboundTransactionRequestType.PurchaseOrder => _outboundCustomerOptions.PurchaseOrder,
        OutboundTransactionRequestType.CreateOrder => _outboundCustomerOptions.CreateOrder,
        OutboundTransactionRequestType.ProductMaster => _outboundCustomerOptions.ProductMaster,
        _ => new List<string>()
    };

    private static string? ToDocumentType(string functionName) => functionName switch
    {
        OutboundTransactionRequestType.PurchaseOrder => TransactionOrderType.PoCreate,
        OutboundTransactionRequestType.CreateOrder => TransactionOrderType.OrderCreate,
        OutboundTransactionRequestType.ReturnOrder => TransactionOrderType.ReturnOrder,
        OutboundTransactionRequestType.ProductMaster => TransactionOrderType.SKU,
        _ => null
    };


    private ValidCustomerLists GetValidCustomers()
    {
        return new ValidCustomerLists
        {
            ReturnOrderCustomer = GetCustomerForDocumentType(OutboundTransactionRequestType.ReturnOrder),
            PurchaseOrderCustomer = GetCustomerForDocumentType(OutboundTransactionRequestType.PurchaseOrder),
            CreateOrderCustomer = GetCustomerForDocumentType(OutboundTransactionRequestType.CreateOrder),
            ProductMasterCustomer = GetCustomerForDocumentType(OutboundTransactionRequestType.ProductMaster),
        };
    }
}
