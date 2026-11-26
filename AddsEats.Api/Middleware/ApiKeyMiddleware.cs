namespace AddsEats.Api.Middleware;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

public class ApiKeyMiddleware
{
    public const string ApiKeyHeaderName = "X-Api-Key";
    private readonly RequestDelegate _next;
    private readonly string _configuredApiKey;

    public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuredApiKey = configuration["AdminApiKey"] ?? "addseats-admin-secret-key";
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey)
            || string.IsNullOrWhiteSpace(extractedApiKey))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(new
            {
                statusCode = StatusCodes.Status401Unauthorized,
                error = "Unauthorized",
                message = "API Key is missing from 'X-Api-Key' header."
            });
            return;
        }

        if (!string.Equals(extractedApiKey, _configuredApiKey, StringComparison.Ordinal))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(new
            {
                statusCode = StatusCodes.Status401Unauthorized,
                error = "Unauthorized",
                message = "Invalid API Key provided."
            });
            return;
        }

        await _next(context);
    }
}
