using Microsoft.AspNetCore.Diagnostics;
using SendGrid.Helpers.Errors.Model;
using BadRequestException = SendGrid.Helpers.Errors.Model.BadRequestException;

namespace dotNet_backend.Exceptions.GlobalExceptionHandler;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;
    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        (int statusCode, string errorMessage) = exception switch
        {
            ForbiddenException => (StatusCodes.Status403Forbidden, exception.Message),
            NotFoundException => (StatusCodes.Status404NotFound, exception.Message),
            BadRequestException => (StatusCodes.Status400BadRequest, exception.Message),
            UnauthorizedException => (StatusCodes.Status401Unauthorized, exception.Message),
            _ => (StatusCodes.Status500InternalServerError, $"An error occurred while processing your request " +
                                                            $"ERROR: {exception.Message}")
        };
        _logger.LogError(exception, exception.Message);
        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(new {errorMessage}, cancellationToken);
        return true;
    }

}