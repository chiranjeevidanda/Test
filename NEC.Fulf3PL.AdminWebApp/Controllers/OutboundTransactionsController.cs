using MediatR;
using Microsoft.AspNetCore.Mvc;
using NEC.Fulf3PL.AdminWebApp.Extensions;
using NEC.Fulf3PL.Application.Admin.Dtos;
using NEC.Fulf3PL.AdminWebApp.Models;
using NEC.Fulf3PL.Application.Admin.Query;
using NEC.Fulf3PL.Core.Entities.Admin;

namespace NEC.Fulf3PL.AdminWebApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OutboundTransactionsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    [ProducesResponseType(typeof(PaginationResponseDto<OutboundResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPaginatedAsync([FromQuery] OutboundTransactionsListSearchFilterDto request)
    {
        request.DateTo = request.DateTo?.AdjustTimeIfZero();
        var query = new ListOutboundTransactionsQuery(request, GetDefaultStartDate());
        var docs = await _mediator.Send(query);
        var response = new PaginationResponseDto<OutboundResponseDto>(request, docs);
        return Ok(response);
    }


    [HttpGet("export")]
    [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> ExportAsync([FromQuery] OutboundTransactionsListSearchFilterDto request)
    {
        request.DateTo = request.DateTo?.AdjustTimeIfZero();
        var fileName = $"KatoenOutboundTransactionLog{DateTime.Now:dd_MM_yyyy_HH_mm}.xlsx";
        var query = new ExportOutboundTransactionsQuery(request, GetDefaultStartDate());
        var content = await _mediator.Send(query);
        if(content!=null)
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