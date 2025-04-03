using MediatR;
using NEC.Fulf3PL.Application.Admin.Dtos;
using NEC.Fulf3PL.Application.Admin.Services;
using NEC.Fulf3PL.Application.Common;
using NEC.Fulf3PL.Application.Outbound.Implementation.KTN.DTO;
using NEC.Fulf3PL.Core.Common.Admin;
using Newtonsoft.Json;

namespace NEC.Fulf3PL.Application.Admin.Query;

public record ExportProductMasterQuery(
    OutboundTransactionsListSearchFilterDto FilterDto,
    DateTime DefaultStartDate) : IRequest<byte[]>;

public class ExportProductMasterQueryHandler : IRequestHandler<ExportProductMasterQuery, byte[]>
{
    private readonly IAdminOutboundRequestsQueryService _queryService;
    private readonly ISpreadSheetService _spreadSheetService;


    public ExportProductMasterQueryHandler(IAdminOutboundRequestsQueryService queryService,
        ISpreadSheetService spreadSheetService)
    {
        _queryService = queryService;
        _spreadSheetService = spreadSheetService;
    }
    public record SkuDetails(string ModifiedDate, IEnumerable<ProductMasterDTO> RequestData);

    public async Task<byte[]> Handle(ExportProductMasterQuery request, CancellationToken cancellationToken)
    {
        request.FilterDto.Take = int.MaxValue;
        request.FilterDto.DocumentType = OutboundTransactionRequestType.ProductMaster;
        var paginationResponse = await _queryService.ListDocuments(request.FilterDto, request.DefaultStartDate);

        if (paginationResponse.Results.Any())
        {
            var transactions = paginationResponse.Results.Select(x => new OutboundProductDetailsDto
            {
                Status = x.Status,
                ModifiedDate = x.ModifiedDate,
                ProductDetails = JsonConvert.DeserializeObject<ProductMasterDTO>(x.RequestData),
                Provider = x.Provider
            });

            return _spreadSheetService.Export(request.FilterDto.DocumentType, transactions);
        }
        throw new InvalidOperationException();
    }
}