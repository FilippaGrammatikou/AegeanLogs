using AegeanLogs.Application.PortCalls.Create;
using Microsoft.AspNetCore.Mvc;

namespace AegeanLogs.Api.Controllers;

[ApiController]
[Route("api/port-calls")]
public sealed class PortCallsController : ControllerBase
{
    private readonly CreatePortCallService _createPortCallService;
    public PortCallsController(CreatePortCallService createPortCallService)
    {
        _createPortCallService = createPortCallService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreatePortCallResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CreatePortCallResponse>> CreateAsync([FromBody] CreatePortCallRequest request, CancellationToken cancellationToken)
    {
        var response = await _createPortCallService.CreateAsync(request, cancellationToken);
        return StatusCode(StatusCodes.Status201Created, response);
    }
}
