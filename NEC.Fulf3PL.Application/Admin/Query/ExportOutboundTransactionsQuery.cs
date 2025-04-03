using MediatR;
using NEC.Fulf3PL.Application.Admin.Dtos;
using NEC.Fulf3PL.Application.Admin.Services;
using NEC.Fulf3PL.Application.Common;

namespace NEC.Fulf3PL.Application.Admin.Query;

public record ExportOutboundTransactionsQuery(
    OutboundTransactionsListSearchFilterDto FilterDto,
    DateTime DefaultStartDate) : IRequest<byte[]>;

public class ExportOutboundTransactionsQueryHandler : IRequestHandler<ExportOutboundTransactionsQuery, byte[]>
{
    private readonly IAdminOutboundRequestsQueryService _queryService;
    private readonly ISpreadSheetService _spreadSheetService;


    public ExportOutboundTransactionsQueryHandler(IAdminOutboundRequestsQueryService queryService,
        ISpreadSheetService spreadSheetService)
    {
        _queryService = queryService;
        _spreadSheetService = spreadSheetService;
    }

    public async Task<byte[]?> Handle(ExportOutboundTransactionsQuery request, CancellationToken cancellationToken)
    {
        request.FilterDto.Take = int.MaxValue;
        var paginationResponse = await _queryService.ListDocuments(request.FilterDto, request.DefaultStartDate);
        if (paginationResponse.Results != null && paginationResponse.Results.Any())
        {
            return _spreadSheetService.Export(request.FilterDto.DocumentType, paginationResponse.Results);
        }
        return null;
    }
}