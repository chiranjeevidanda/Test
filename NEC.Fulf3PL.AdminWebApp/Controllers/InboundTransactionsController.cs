using MediatR;
using Microsoft.AspNetCore.Mvc;
using NEC.Fulf3PL.AdminWebApp.Extensions;
using NEC.Fulf3PL.Application.Admin.Dtos;
using NEC.Fulf3PL.AdminWebApp.Models;
using NEC.Fulf3PL.Application.Admin.Query;

namespace NEC.Fulf3PL.AdminWebApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InboundTransactionsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    [ProducesResponseType(typeof(PaginationResponseDto<InboundTransactionsDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPaginatedAsync([FromQuery] InboundTransactionsListSearchFilterDto request)
    {
        request.DateTo = request.DateTo?.AdjustTimeIfZero();
        var query = new ListSapTransactionsQuery(request, GetDefaultStartDate());
        var docs = await _mediator.Send(query);
        var response = new PaginationResponseDto<InboundTransactionsDto>(request, docs);
        return Ok(response);
    }

    [HttpGet("export")]
    [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> ExportAsync([FromQuery] InboundTransactionsListSearchFilterDto request)
    {
        request.DateTo = request.DateTo?.AdjustTimeIfZero();
        var fileName = $"KatoenInboundTransactionLog{DateTime.Now:dd_MM_yyyy_HH_mm}.xlsx";
        var query = new ExportSapTransactionsQuery(request, GetDefaultStartDate());
        var content = await _mediator.Send(query);
        if (content != null)
        {
            return File(content,
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            fileName);
        }
        else
        {
            return NoContent();
        }
    }

    private static DateTime GetDefaultStartDate()
    {
        return DateTime.UtcNow.AddDays(-7);
    }
}