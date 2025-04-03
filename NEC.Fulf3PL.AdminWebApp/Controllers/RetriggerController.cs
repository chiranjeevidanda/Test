using MediatR;
using Microsoft.AspNetCore.Mvc;
using NEC.Fulf3PL.AdminWebApp.Extensions;
using NEC.Fulf3PL.AdminWebApp.Models;
using NEC.Fulf3PL.Application.Admin.Command;
using NEC.Fulf3PL.Application.Admin.Dtos;
using NEC.Fulf3PL.Application.Admin.Query;

namespace NEC.Fulf3PL.AdminWebApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RetriggerController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    [ProducesResponseType(typeof(RetriggerResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> PostAsync([FromBody] RetriggerRequestDto request)
    {
        request.DateTo = request.DateTo?.AdjustTimeIfZero();
        var response = await _mediator.Send(new RetriggerFailedDocumentCommand(request));       
        return Ok(response);
    }

    [HttpGet("{eventId}")]
    public async Task<IActionResult> GetById(string eventId)
    {
        var payload = await _mediator.Send(new GetRetriggerPayloadByIdQuery(eventId));

        return Ok(payload);
    }
}
