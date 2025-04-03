using MediatR;
using Microsoft.AspNetCore.Mvc;
using NEC.Fulf3PL.AdminWebApp.Extensions;
using NEC.Fulf3PL.Application.Admin.Dtos;
using NEC.Fulf3PL.AdminWebApp.Models;
using NEC.Fulf3PL.Application.Admin.Query;

namespace NEC.Fulf3PL.AdminWebApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DashboardController(IMediator mediator) : ControllerBase
{
    [HttpGet("OutboundTransaction")]
    [ProducesResponseType(typeof(PaginationResponseDto<InboundTransactionsDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> OutboundTransaction([FromQuery] DashboardFilterDto request)
    {
        var query = new OutboundTransactionDashboardQuery();
        var docs = await mediator.Send(query);
        var response = new PaginationResponseDto<string>(request, docs);
        return Ok(response);
    }

    [HttpGet("InboundTransaction")]
    [ProducesResponseType(typeof(PaginationResponseDto<InboundTransactionsDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> InboundTransaction([FromQuery] DashboardFilterDto request)
    {
        var query = new InboundTransactionDashboardQuery();
        var docs = await mediator.Send(query);
        var response = new PaginationResponseDto<InboundServicebusStatistics>(request, docs);
        return Ok(response);
    }
}