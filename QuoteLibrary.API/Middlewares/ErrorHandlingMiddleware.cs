
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using QuoteLibrary.API.Enums;
using System.Net;

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

            var (statusCode, errorType, detail) = GetErrorDetails(ex);

            ProblemDetails problemDetails = new()
            {
                Status = (int)statusCode,
                Type = errorType.ToString(),
                Title = "Server error",
                Detail = detail
            };

            string json = JsonSerializer.Serialize(problemDetails);

            await context.Response.WriteAsync(json);

        }

        private (HttpStatusCode statusCode, ErrorType errorType, string detail) GetErrorDetails(Exception ex)
        {
            return ex switch
            {
                ArgumentNullException => (HttpStatusCode.BadRequest, ErrorType.NullReference, "Se lanza cuando un argumento pasado a un método es nulo y no se permite."),
                ArgumentException => (HttpStatusCode.BadRequest, ErrorType.Argument, "Se lanza cuando se produce un argumento no válido en un método."),
                InvalidOperationException => (HttpStatusCode.Conflict, ErrorType.InvalidOperation, "Se produce cuando el estado actual de un objeto no permite que se ejecute una operación específica."),
                UnauthorizedAccessException => (HttpStatusCode.Unauthorized, ErrorType.Unauthorized, "Se produce cuando no tiene permisos requeridos para realizar una acción"),
                System.Security.SecurityException => (HttpStatusCode.Forbidden, ErrorType.Forbidden, "Se produce cuando no tiene permisos para acceder a un recurso."),
                KeyNotFoundException => (HttpStatusCode.NotFound, ErrorType.NotFound, "Se produce cuando la llave especificada para acceder a un elemento de la lista no hace match con ningún elemento."),
                _ => (HttpStatusCode.InternalServerError, ErrorType.InternalServerError, "Se produce al tener un error interno con el servidor.")
            };
        }
    }
}
