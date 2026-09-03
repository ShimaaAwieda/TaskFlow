using System.Net;
using TaskFlow.Application.Exceptions;

namespace TaskFlow.API.Middlewares
{
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
            catch (BadRequestException ex)
            {
                _logger.LogWarning(ex, "BadRequest exception caught in middleware");
                await HandleExceptionAsync(context, HttpStatusCode.BadRequest, ex.Message);
            }
            catch (ConflictException ex)
            {
                _logger.LogWarning(ex, "Conflict exception caught in middleware");
                await HandleExceptionAsync(context, HttpStatusCode.Conflict, ex.Message);
            }
            catch (ForbiddenException ex)
            {
                _logger.LogWarning(ex, "Forbidden exception caught in middleware");
                await HandleExceptionAsync(context, HttpStatusCode.Forbidden, ex.Message);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex, "NotFound exception caught in middleware");
                await HandleExceptionAsync(context, HttpStatusCode.NotFound, ex.Message);
            }
            catch (UnauthorizedException ex)
            {
                _logger.LogWarning(ex, "Unauthorized exception caught in middleware");
                await HandleExceptionAsync(context, HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Unexpected exception caught in middleware");
                await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, "An unexpected error occurred");
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var errorResponse = new
            {
                statusCode = (int)statusCode,
                message = message
            };

            await context.Response.WriteAsJsonAsync(errorResponse);
           
        }
    }
}
