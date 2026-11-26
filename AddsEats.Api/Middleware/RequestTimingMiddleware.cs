namespace AddsEats.Api.Middleware;

using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

public class RequestTimingMiddleware
{
    private const long SlowRequestThresholdMs = 1000;
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestTimingMiddleware> _logger;

    public RequestTimingMiddleware(RequestDelegate next, ILogger<RequestTimingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        try
        {
            await _next(context);
        }
        finally
        {
            stopwatch.Stop();
            var elapsedMs = stopwatch.ElapsedMilliseconds;
            var statusCode = context.Response.StatusCode;
            var method = context.Request.Method;
            var path = context.Request.Path;
            var correlationId = context.TraceIdentifier;

            if (elapsedMs > SlowRequestThresholdMs)
            {
                _logger.LogWarning(
                    "[{CorrelationId}] HTTP {Method} {Path} responded {StatusCode} in {ElapsedMs}ms (SLOW REQUEST)",
                    correlationId, method, path, statusCode, elapsedMs);
            }
            else
            {
                _logger.LogInformation(
                    "[{CorrelationId}] HTTP {Method} {Path} responded {StatusCode} in {ElapsedMs}ms",
                    correlationId, method, path, statusCode, elapsedMs);
            }
        }
    }
}

public static class RequestTimingMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestTiming(this IApplicationBuilder app)
    {
        return app.UseMiddleware<RequestTimingMiddleware>();
    }
}
