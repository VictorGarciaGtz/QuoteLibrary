namespace QuoteLibrary.API.Middlewares
{
    public class ApiVersioningMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiVersioningMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            // Extraer la versión de los encabezados o query string
            if (httpContext.Request.Headers.TryGetValue("x-api-version", out var versionHeader))
            {
                httpContext.Request.Path = $"/api/v{versionHeader}{httpContext.Request.Path}";
            }
            else if (httpContext.Request.Query.TryGetValue("api-version", out var versionQuery))
            {
                httpContext.Request.Path = $"/api/v{versionQuery}{httpContext.Request.Path}";
            }

            await _next(httpContext);
        }
    }
}
