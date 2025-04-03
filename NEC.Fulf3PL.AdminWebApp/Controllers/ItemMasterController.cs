using MediatR;
using Microsoft.AspNetCore.Mvc;
using NEC.Fulf3PL.AdminWebApp.Extensions;
using NEC.Fulf3PL.AdminWebApp.Models;
using NEC.Fulf3PL.Application.Admin.Command;
using NEC.Fulf3PL.Application.Admin.Dtos;
using NEC.Fulf3PL.Application.Admin.Query;
using NEC.Fulf3PL.Application.Admin.Services;
using NEC.Fulf3PL.Application.Outbound.Implementation.KTN.DTO;

namespace NEC.Fulf3PL.AdminWebApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ItemMasterController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;


    [HttpGet]
    [ProducesResponseType(typeof(PaginationResponseDto<ItemMasterDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPaginatedAsync([FromQuery] OutboundTransactionsListSearchFilterDto request)
    {
        var query = new ProductMasterQuery(request, GetDefaultStartDate());
        var docs = await _mediator.Send(query);
        var response = new PaginationResponseDto<ProductMasterDetails>(request, docs);
        return Ok(response);

    }

    [HttpPost]
    [ProducesResponseType(typeof(RetriggerResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> PostAsync([FromBody] RetriggerRequestDto request)
    {
        request.DateTo = request.DateTo?.AdjustTimeIfZero();
        var response = await _mediator.Send(new RetriggerSkuCommandCommand(request));        
        return Ok(response);
    }

    [HttpGet("export")]
    [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> ExportAsync([FromQuery] OutboundTransactionsListSearchFilterDto request)
    {
        request.DateTo = request.DateTo?.AdjustTimeIfZero();
        var fileName = $"KatoenSKU{DateTime.Now:dd_MM_yyyy_HH_mm}.xlsx";
        var query = new ExportProductMasterQuery(request, GetDefaultStartDate());
        var content = await _mediator.Send(query);
        return File(content,
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            fileName);
    }

    private static DateTime GetDefaultStartDate()
    {
        return DateTime.UtcNow.AddDays(-7);
    }
}
