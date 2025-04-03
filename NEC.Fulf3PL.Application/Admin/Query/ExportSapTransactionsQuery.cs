using MediatR;
using NEC.Fulf3PL.Application.Admin.Dtos;
using NEC.Fulf3PL.Application.Admin.Services;
using NEC.Fulf3PL.Application.Common;

namespace NEC.Fulf3PL.Application.Admin.Query;

public record ExportSapTransactionsQuery(
    InboundTransactionsListSearchFilterDto FilterDto,
    DateTime DefaultStartDate) : IRequest<byte[]>;

public class ExportSapTransactionsQueryHandler : IRequestHandler<ExportSapTransactionsQuery, byte[]>
{
    private readonly ISapTransactionsQueryService _queryService;
    private readonly ISpreadSheetService _spreadSheetService;

    public ExportSapTransactionsQueryHandler(ISapTransactionsQueryService queryService,
        ISpreadSheetService spreadSheetService)
    {
        _queryService = queryService;
        _spreadSheetService = spreadSheetService;
    }

    public async Task<byte[]?> Handle(ExportSapTransactionsQuery request, CancellationToken cancellationToken)
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