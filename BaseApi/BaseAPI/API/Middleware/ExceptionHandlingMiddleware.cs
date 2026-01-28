using System.Net;
using System.Text.Json;
using BaseAPI.Application.Common.Exceptions;

namespace BaseAPI.API.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        var response = context.Response;

        var errorResponse = new ErrorResponse
        {
            Success = false
        };

        switch (exception)
        {
            case ValidationException validationException:
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                errorResponse.Message = "Validation failed";
                errorResponse.Errors = validationException.Errors
                    .SelectMany(e => e.Value.Select(v => $"{e.Key}: {v}"))
                    .ToList();
                _logger.LogWarning("Validation exception: {Message}", exception.Message);
                break;

            case NotFoundException notFoundException:
                response.StatusCode = (int)HttpStatusCode.NotFound;
                errorResponse.Message = notFoundException.Message;
                _logger.LogWarning("Not found exception: {Message}", exception.Message);
                break;

            case BadRequestException badRequestException:
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                errorResponse.Message = badRequestException.Message;
                _logger.LogWarning("Bad request exception: {Message}", exception.Message);
                break;

            case UnauthorizedAccessException:
                response.StatusCode = (int)HttpStatusCode.Unauthorized;
                errorResponse.Message = "Unauthorized access";
                _logger.LogWarning("Unauthorized access: {Message}", exception.Message);
                break;

            default:
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                errorResponse.Message = "An internal server error occurred";
                _logger.LogError(exception, "Unhandled exception: {Message}", exception.Message);
                break;
        }

        errorResponse.StatusCode = response.StatusCode;

        var result = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        await response.WriteAsync(result);
    }

    private class ErrorResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<string> Errors { get; set; } = new();
        public int StatusCode { get; set; }
    }
}
