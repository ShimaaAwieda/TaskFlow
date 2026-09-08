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
            catch(Exception ex)
            {
                var (statusCode, message) = ex switch
                {
                    BadRequestException badRequestEx => (HttpStatusCode.BadRequest, badRequestEx.Message),
                    ConflictException conflictEx => (HttpStatusCode.Conflict, conflictEx.Message),
                    ForbiddenException forbiddenEx => (HttpStatusCode.Forbidden, forbiddenEx.Message),
                    NotFoundException notFoundEx => (HttpStatusCode.NotFound, notFoundEx.Message),
                    UnauthorizedException unauthorizedEx => (HttpStatusCode.Unauthorized, unauthorizedEx.Message),
                    _ => (HttpStatusCode.InternalServerError, "An unexpected error occurred")
                };

                _logger.LogWarning(ex, "Exception occurred: {Message}", ex.Message);

                await HandleExceptionAsync(context, statusCode, message);
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
