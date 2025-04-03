using MediatR;
using NEC.Fulf3PL.Application.Admin.Dtos;
using NEC.Fulf3PL.Application.Admin.Services;
using NEC.Fulf3PL.Application.Common;
using NEC.Fulf3PL.Application.Outbound.Implementation.KTN.DTO;
using NEC.Fulf3PL.Application.Outbound.Implementation.GEODIS.DTO;
using NEC.Fulf3PL.Application.Queries.Pagination;
using NEC.Fulf3PL.Core.Common.Admin;
using NEC.Fulf3PL.Core.Entities.Admin;
using Newtonsoft.Json;
using static NEC.Fulf3PL.Core.Common.Enums;
using NEC.Fulf3PL.Core.Common;

namespace NEC.Fulf3PL.Application.Admin.Query;

public record ProductMasterQuery(
    OutboundTransactionsListSearchFilterDto FilterDto,
    DateTime DefaultStartDate) : IRequest<PaginationResponseModel<ProductMasterDetails>>;

public class ProductMasterQueryHandler : IRequestHandler<ProductMasterQuery, PaginationResponseModel<ProductMasterDetails>>
{
    private readonly IAdminOutboundRequestsQueryService _queryService;


    public ProductMasterQueryHandler(IAdminOutboundRequestsQueryService queryService,
        ISpreadSheetService spreadSheetService)
    {
        _queryService = queryService;
    }

    public async Task<PaginationResponseModel<ProductMasterDetails>> Handle(ProductMasterQuery request, CancellationToken cancellationToken)
    {
        request.FilterDto.DocumentType = OutboundTransactionRequestType.ProductMaster;

        var paginationResponse = await _queryService.ListDocuments(request.FilterDto, request.DefaultStartDate);
        if (paginationResponse.Results.Any())
        {

            var response = paginationResponse.Results.Select(x => new ProductMasterDetails
            {
                Status = x.Status,
                ProductDetails = GetProductDetails(x?.RequestData) ,
                ModifiedDate = x.ModifiedDate
            });
            var result = new PaginationResponseModel<ProductMasterDetails>(new List<ProductMasterDetails>(response), paginationResponse.Total);
            return result;
        }
        else
        {
            return new PaginationResponseModel<ProductMasterDetails>(new List<ProductMasterDetails>(), 0);
        }
    }
   
    private SkuDetails? GetProductDetails(string? requestData)
    {
        var data = JsonConvert.DeserializeObject<Outbound.Implementation.KTN.DTO.ProductMasterDTO>(requestData);
        return new SkuDetails()
        {
            Customer = data?.Customer ?? string.Empty,
            Code = data?.Code ?? string.Empty,
            Description = data?.Description ?? string.Empty,
            StockKeepingUnit = data?.StockKeepingUnit ?? string.Empty,
            ItemGroup = data?.ItemGroup ?? string.Empty,
            CodeOfGoods = data?.CodeOfGoods ?? string.Empty,
            Reference1 = data?.Reference1 ?? string.Empty,
            Reference3 = data?.Reference3 ?? string.Empty,
            Model = data?.Model ?? string.Empty,
            Design = data?.Design ?? string.Empty,
            Size = data?.Size ?? string.Empty,
            Color = data?.Color ?? string.Empty,
            Season = data?.Season ?? string.Empty
        };
    }
}