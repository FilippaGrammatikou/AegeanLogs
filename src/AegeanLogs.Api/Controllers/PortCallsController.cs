using AegeanLogs.Application.PortCalls.Create;
using AegeanLogs.Application.PortCalls.GetById;
using Microsoft.AspNetCore.Mvc;

namespace AegeanLogs.Api.Controllers;

[ApiController]
[Route("api/port-calls")]
public sealed class PortCallsController : ControllerBase
{
    private readonly CreatePortCallService _createPortCallService;
    private readonly GetPortCallByIdService _getPortCallByIdService;

    public PortCallsController(CreatePortCallService createPortCallService, GetPortCallByIdService getPortCallByIdService)
    {
        _createPortCallService = createPortCallService;
        _getPortCallByIdService = getPortCallByIdService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreatePortCallResponse),StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status500InternalServerError)]

    public async Task<ActionResult<CreatePortCallResponse>> CreateAsync(
        [FromBody] CreatePortCallRequest request,CancellationToken cancellationToken)
    {
        var response = await _createPortCallService.CreateAsync(request,cancellationToken);
        return CreatedAtAction(nameof(GetByIdAsync),new { id = response.Id },response);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(GetPortCallByIdResponse),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status500InternalServerError)]

    public async Task<ActionResult<GetPortCallByIdResponse>> GetByIdAsync(int id,CancellationToken cancellationToken)
    {
        var response = await _getPortCallByIdService.GetByIdAsync(id,cancellationToken);
        return Ok(response);
    }
}
