namespace AddsEats.Api.Middleware;

using AddsEats.Api.Services.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

/// <summary>
/// Exercise 5: Concurrency and Scoped Service Injection demonstration.
/// Shows why scoped services MUST be injected into InvokeAsync, NOT the constructor.
/// </summary>
public class ScopedServiceExperimentMiddleware
{
    private readonly RequestDelegate _next;

    // NOTE: In the buggy scenario, injecting `IScopedStateService` into this constructor
    // causes a Captive Dependency (Singleton holds Scoped service) and root provider scope validation failure.
    public ScopedServiceExperimentMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    // CORRECT: Scoped services injected into InvokeAsync are resolved per-request from context.RequestServices.
    public async Task InvokeAsync(HttpContext context, IScopedStateService scopedService)
    {
        if (context.Request.Path.StartsWithSegments("/api/diagnostics/concurrency-test"))
        {
            var client = context.Request.Query["client"].ToString();
            if (string.IsNullOrWhiteSpace(client))
            {
                client = "Client-" + Guid.NewGuid().ToString()[..4];
            }

            var result = await scopedService.ProcessAsync(client);
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(new
            {
                client,
                result,
                scopedInstanceId = scopedService.InstanceId,
                resolvedVia = "InvokeAsync (Per-Request Scope)"
            });
            return;
        }

        await _next(context);
    }
}

public static class ScopedServiceExperimentMiddlewareExtensions
{
    public static IApplicationBuilder UseScopedServiceExperiment(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ScopedServiceExperimentMiddleware>();
    }
}
