using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Shared.Middlewares;

public class GlobalExceptionHandler
{
    private const string ContentType = "application/json";
    private readonly RequestDelegate _next;

    public GlobalExceptionHandler(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception exception)
        {
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            httpContext.Response.ContentType = ContentType;

            var response = new Response<string>(string.Empty, exception.Message);

            var jsonResponse = JsonSerializer.Serialize(response);

            await httpContext.Response.WriteAsync(jsonResponse);
        }
    }
}