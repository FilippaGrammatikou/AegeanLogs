using AegeanLogs.Application.Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ApplicationValidationException =AegeanLogs.Application.Common.Exceptions.ValidationException;

namespace AegeanLogs.Api.Middleware;
public sealed class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;
    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext,Exception exception,CancellationToken cancellationToken)
    {
        var (statusCode, title) = exception switch
        {
            ApplicationValidationException =>(StatusCodes.Status400BadRequest,"Validation failed"),
            ForbiddenActionException =>(StatusCodes.Status403Forbidden,"Forbidden"),
            NotFoundException =>(StatusCodes.Status404NotFound,"Resource not found"),
            ConflictException =>(StatusCodes.Status409Conflict,"Conflict"),

            _ =>(StatusCodes.Status500InternalServerError,"Internal server error")
        };

        if (statusCode == StatusCodes.Status500InternalServerError)
        {
            _logger.LogError(exception,"An unexpected exception occurred while processing {Method} {Path}.",
                                          httpContext.Request.Method,
                                          httpContext.Request.Path);
        }

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = statusCode == StatusCodes.Status500InternalServerError
                                                  ? "An unexpected error occurred while processing the request."
                                                  : exception.Message,
            Instance = httpContext.Request.Path
        };

        problemDetails.Extensions["traceId"] =httpContext.TraceIdentifier;
        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        return true;
    }
}
