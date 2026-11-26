namespace AddsEats.Api.Filters;

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Text.Json;

/// <summary>
/// Exercise 7: Action filter logging the executing action and its bound arguments.
/// 
/// WHY THIS COULD NOT BE MIDDLEWARE (ONE-LINE EXPLANATION):
/// Middleware executes before MVC routing and model binding take place, meaning the selected action descriptor
/// and the deserialized, strongly-typed arguments do not exist in the HttpContext yet.
/// </summary>
public class AuditActionFilter : IActionFilter
{
    private readonly ILogger<AuditActionFilter> _logger;

    public AuditActionFilter(ILogger<AuditActionFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var actionName = context.ActionDescriptor.DisplayName;
        var correlationId = context.HttpContext.TraceIdentifier;

        string serializedArgs;
        try
        {
            serializedArgs = JsonSerializer.Serialize(context.ActionArguments);
        }
        catch
        {
            serializedArgs = string.Join(", ", context.ActionArguments.Keys);
        }

        _logger.LogInformation(
            "[{CorrelationId}] [Audit] Executing Action: {ActionName} with Arguments: {Arguments}",
            correlationId, actionName, serializedArgs);
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        var actionName = context.ActionDescriptor.DisplayName;
        var correlationId = context.HttpContext.TraceIdentifier;

        if (context.Exception != null)
        {
            _logger.LogError(
                context.Exception,
                "[{CorrelationId}] [Audit] Action {ActionName} failed with an unhandled exception.",
                correlationId, actionName);
        }
        else
        {
            _logger.LogInformation(
                "[{CorrelationId}] [Audit] Action {ActionName} executed successfully.",
                correlationId, actionName);
        }
    }
}
