using System.Text.Json;

namespace GWSales.WebApi.Middlewares;

public class HundleDateOnlyConvertExceptionMiddleware
{
    private readonly RequestDelegate _next;
    public HundleDateOnlyConvertExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (JsonException error)
        {
            await HandleExceptionAsync(context, error);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, JsonException exception)
    {
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        context.Response.ContentType = "application/json";

        var errorMessage = new { message = exception.Message };
        var json = JsonSerializer.Serialize(errorMessage);
        return context.Response.WriteAsync(json);
    }
}
