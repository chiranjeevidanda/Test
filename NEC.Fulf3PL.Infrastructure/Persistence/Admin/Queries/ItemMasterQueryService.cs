using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Extensions.Options;
using NEC.Fulf3PL.Infrastructure.Persistence.CosmosDB.Options;
using NEC.Fulf3PL.Application.Admin.Dtos;
using NEC.Fulf3PL.Application.Admin.Services;
using NEC.Fulf3PL.Application.Queries.Pagination;
using NEC.Fulf3PL.Core.Entities.Admin;
using NEC.Fulf3PL.Infrastructure.Persistence.CosmosDB;
using NEC.Fulf3PL.Infrastructure.Persistence.CosmosDB.Extensions;
using System.Data;
using System.Linq.Expressions;
using NEC.Fulf3PL.Application.Outbound.Implementation.KTN.DTO;
using NEC.Fulf3PL.Application.Outbound.Interface;
using NEC.Fulf3PL.Application.Outbound.Implementation.KTN.Models;
using NEC.Fulf3PL.Core.Common;
using LogicExtensions;
using NEC.Fulf3PL.Application.Outbound.Interface.COMMON;
using CommonModel = NEC.Fulf3PL.Application.Outbound.Implementation.COMMON.Models;

namespace NEC.Fulf3PL.Infrastructure.Persistence.Admin.Queries;

public class ItemMasterQueryService : GenericQueryService<ItemMaster>, IItemMasterQueryService
{
    private readonly IItemMasterService<OutboundConverterModel> _itemMasterService;
    public ItemMasterQueryService(CosmosClient cosmosClient, IOptions<CosmosDbConfigOptions> options, IItemMasterService<OutboundConverterModel> itemMasterService, IItemMasterServiceCommon<CommonModel.OutboundConverterModel> geodisItemMasterService)
        : base(cosmosClient, options.Value, options.Value.ItemMasterContainer)
    {
        _itemMasterService = itemMasterService;
    }

    public async Task<PaginationResponseModel<ItemMasterDto>> ListDocuments(ItemMasterListSearchFilterDto filterDto)
    {
        var filterExpressions = new List<Expression<Func<ItemMaster, bool>>>();

        if (!string.IsNullOrEmpty(filterDto.SkuIds))
        {
            string[] skuIds = filterDto.SkuIds.Replace(" ", ",").Split(",", StringSplitOptions.RemoveEmptyEntries);

            filterExpressions.Add(x => skuIds.Contains(x.Data.ProductCode));
        }

        IQueryable<ItemMaster> queryable = _container.GetItemLinqQueryable<ItemMaster>();

        queryable = queryable.ApplyFilterExpressions(filterExpressions);

        var total = await queryable.CountAsync();

        var sortExpression = GetSortExpression(filterDto.SortBy);

        queryable = filterDto.IsAscending() ? queryable.OrderBy(sortExpression) : queryable.OrderByDescending(sortExpression);

        if (filterDto.Skip > 0)
        {
            queryable = queryable.Skip(filterDto.Skip);
        }

        queryable = queryable.Take(filterDto.Take);

        var query = queryable.Select(x => new
        {
            x.Id,
            x.Data,
            x.Provider,
            x.LoggedOn,
        });

        var items = await query.ToFeedIterator().ToListAsync();
        var mappedItems = items.Select(x => new ItemMasterDto
        {
            SKUId = x.Data?.ProductCode ?? string.Empty,
            ProductDescription = x.Data?.ProductDescription ?? string.Empty,

        });
        var response = new PaginationResponseModel<ItemMasterDto>(mappedItems, total);
        return response;
    }

    public static Expression<Func<ItemMaster, object?>> GetSortExpression(string? sortBy)
    {
        sortBy = sortBy?.ToUpperInvariant();
        Expression<Func<ItemMaster, object?>> sortExpression = sortBy switch
        {
            "LOGGEDON" => (ItemMaster pc) => pc.LoggedOn,
            _ => (ItemMaster pc) => pc.LoggedOn
        };
        return sortExpression;
    }

    public async Task<ExecuteResult<OutboundConverterModel>> MapItemMasterData(string productCode, string plant, string provider)
    {
        var result = new ExecuteResult<OutboundConverterModel>();

        if (!string.IsNullOrEmpty(productCode))
        {
            var queryDefinition = new QueryDefinition($"SELECT * FROM c where c.data.productCode='{productCode}' and  c.data.plant='{plant}' and c.provider='{provider}'order by c.updatedOn desc");

            var skuItems = await _container.GetItemsAsync<ItemMaster>(queryDefinition);
            if (skuItems != null)
            {
                var deliveryMessage = new OutboundDeliveryMessage();
                deliveryMessage.InboundEndpoint = Enums.OutboundAPIEndpoint.ProductMaster.ToString();

                deliveryMessage.RequestData = skuItems
                                        .Select(item => item.Data)
                                        .Where(data => data != null)
                                        .GroupBy(data => data.Plant)
                                        .Select(group => group.OrderByDescending(data => data.TimeStamp).FirstOrDefault())
                                        .ToList();

                var response = await _itemMasterService.MapData(deliveryMessage);

                return response;
            }
        }

        return result;
    }

}
