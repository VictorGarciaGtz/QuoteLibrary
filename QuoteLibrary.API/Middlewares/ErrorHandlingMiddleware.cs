
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace QuoteLibrary.API.Middlewares
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }


        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);   
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occured: {ex.Message}");
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            context.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;

            ProblemDetails problemDetails = new()
            {
                Status = (int)System.Net.HttpStatusCode.InternalServerError,
                Type = "Server error",
                Title = "Server error",
                Detail = "An internal server has ocurred"
            };

            string json = JsonSerializer.Serialize(problemDetails);

            await context.Response.WriteAsync(json);

        }
    }
}
