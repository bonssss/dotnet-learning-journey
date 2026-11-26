namespace AddsEats.Api.Middleware;

using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

public class CorrelationIdMiddleware
{
    public const string CorrelationIdHeaderName = "X-Correlation-Id";
    private readonly RequestDelegate _next;

    public CorrelationIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // 1. Accept incoming X-Correlation-Id or generate a new one
        if (!context.Request.Headers.TryGetValue(CorrelationIdHeaderName, out var correlationId) 
            || string.IsNullOrWhiteSpace(correlationId))
        {
            correlationId = Guid.NewGuid().ToString();
        }

        // 2. Set TraceIdentifier to align ASP.NET Core logs and diagnostics
        context.TraceIdentifier = correlationId.ToString();

        // 3. Return the correlation id on the response headers
        context.Response.OnStarting(() =>
        {
            if (!context.Response.Headers.ContainsKey(CorrelationIdHeaderName))
            {
                context.Response.Headers[CorrelationIdHeaderName] = correlationId;
            }
            return Task.CompletedTask;
        });

        await _next(context);
    }
}

public static class CorrelationIdMiddlewareExtensions
{
    public static IApplicationBuilder UseCorrelationId(this IApplicationBuilder app)
    {
        return app.UseMiddleware<CorrelationIdMiddleware>();
    }
}
