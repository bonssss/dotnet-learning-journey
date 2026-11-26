# Middleware Exercises: AddsEats API

This document details the eight middleware exercises implemented against the **AddsEats API** capstone project.

---

## Exercise 1: `CorrelationIdMiddleware`
* **File:** [`Middleware/CorrelationIdMiddleware.cs`](file:///home/bonsa/Documents/dotnet-backend/AddsEats.Api/Middleware/CorrelationIdMiddleware.cs)
* **Goal:** Accept incoming `X-Correlation-Id` header or generate a new GUID, assign it to `context.TraceIdentifier`, and echo it back in the HTTP response headers.
* **Mechanism:**
  * Checks `context.Request.Headers["X-Correlation-Id"]`. If missing or empty, generates `Guid.NewGuid().ToString()`.
  * Sets `context.TraceIdentifier = correlationId`.
  * Registers a callback with `context.Response.OnStarting()` to append `X-Correlation-Id` to response headers before headers are flushed to the client.

---

## Exercise 2: `RequestTimingMiddleware`
* **File:** [`Middleware/RequestTimingMiddleware.cs`](file:///home/bonsa/Documents/dotnet-backend/AddsEats.Api/Middleware/RequestTimingMiddleware.cs)
* **Goal:** Log HTTP method, path, response status code, and elapsed milliseconds; elevate to `Warning` level if elapsed time exceeds 1000ms.
* **Mechanism:**
  * Starts `Stopwatch.StartNew()` before invoking downstream components via `await _next(context)`.
  * In the `finally` block, computes `stopwatch.ElapsedMilliseconds`.
  * If `elapsedMs > 1000`, logs at `LogLevel.Warning` with `(SLOW REQUEST)`.
  * Otherwise, logs at `LogLevel.Information`.

---

## Exercise 3: Pipeline Ordering & Correlation ID Propagation
* **Configuration:** In [`Program.cs`](file:///home/bonsa/Documents/dotnet-backend/AddsEats.Api/Program.cs):
  ```csharp
  app.UseCorrelationId();
  app.UseRequestTiming();
  ```
* **Why this order?**
  * `CorrelationIdMiddleware` must execute first so `context.TraceIdentifier` is set before `RequestTimingMiddleware` records its log entry.
* **Observed Verification Logs:**
  ```text
  info: AddsEats.Api.Middleware.RequestTimingMiddleware[0]
        [custom-corr-abc-123] HTTP GET /api/v1/menuitem responded 200 in 84ms

  info: AddsEats.Api.Middleware.RequestTimingMiddleware[0]
        [93a02643-e4ff-4d3d-9ee4-66d6a4b20458] HTTP GET /api/v1/menuitem responded 200 in 2ms

  warn: AddsEats.Api.Middleware.RequestTimingMiddleware[0]
        [343dcdf7-5cfc-43d7-b20a-26e2df9ee9b4] HTTP GET /api/diagnostics/slow responded 200 in 1072ms (SLOW REQUEST)
  ```
  The correlation ID is successfully propagated from the incoming request or auto-generated, injected into `TraceIdentifier`, displayed inside the log line, and returned on the response header.

---

## Exercise 4: `ApiKeyMiddleware` & Branch Mapping via `Map`
* **File:** [`Middleware/ApiKeyMiddleware.cs`](file:///home/bonsa/Documents/dotnet-backend/AddsEats.Api/Middleware/ApiKeyMiddleware.cs)
* **Goal:** Short-circuit with `401 Unauthorized` when `X-Api-Key` header is missing or invalid, applied exclusively to `/admin`.
* **Registration:**
  ```csharp
  app.Map("/admin", adminApp =>
  {
      adminApp.UseMiddleware<ApiKeyMiddleware>();
      adminApp.Run(async context =>
      {
          context.Response.ContentType = "application/json";
          await context.Response.WriteAsJsonAsync(new
          {
              status = "Success",
              message = "Welcome to the secure AddsEats Admin area."
          });
      });
  });
  ```
* **Observed Verification:**
  * Request `GET /admin` without header $\rightarrow$ `401 Unauthorized`:
    ```json
    {"statusCode":401,"error":"Unauthorized","message":"API Key is missing from 'X-Api-Key' header."}
    ```
  * Request `GET /admin` with `-H "X-Api-Key: wrong-key"` $\rightarrow$ `401 Unauthorized`:
    ```json
    {"statusCode":401,"error":"Unauthorized","message":"Invalid API Key provided."}
    ```
  * Request `GET /admin` with `-H "X-Api-Key: addseats-admin-secret-key"` $\rightarrow$ `200 OK`:
    ```json
    {"status":"Success","message":"Welcome to the secure AddsEats Admin area."}
    ```
  * Request `GET /api/v1/menuitem` without API key $\rightarrow$ `200 OK` (branching isolates `/admin` without impacting other endpoints).

---

## Exercise 5: Concurrency Bug & Captive Dependency in Convention Middleware

### The Setup
Convention middleware registered with `app.UseMiddleware<TMiddleware>()` is created **once** as a Singleton at application startup during pipeline construction.

### 1. The Bug: Constructor Injection
When a scoped service (e.g., `IScopedStateService` or an EF Core `DbContext`) is injected into the **constructor** of a convention middleware:
```csharp
// BUGGY PATTERN:
public class BuggyMiddleware
{
    private readonly IScopedStateService _scopedService;
    public BuggyMiddleware(RequestDelegate next, IScopedStateService scopedService)
    {
        _scopedService = scopedService; // CAPTIVE DEPENDENCY!
    }
}
```
#### Observations:
1. **Startup Failure in Development Mode (`ValidateScopes = true`):**
   ```text
   System.InvalidOperationException: Cannot resolve scoped service 'IScopedStateService' from root provider.
      at Microsoft.Extensions.DependencyInjection.ServiceLookup.CallSiteValidator.ValidateResolution(...)
   ```
   ASP.NET Core detects that a scoped service is being captured by a singleton at application startup.
2. **Runtime State Corruption under Concurrency (if Scope Validation is Disabled):**
   When two requests hit the endpoint at the exact same moment from two different terminals:
   * Both threads share the **exact same instance** of `IScopedStateService`.
   * Terminal A sets `_currentClientTag = "Terminal-A"`.
   * Terminal B simultaneously runs on another thread and overwrites `_currentClientTag = "Terminal-B"`.
   * Terminal A finishes and reads `_currentClientTag`, returning data belonging to Terminal B!
   * Under EF Core, this manifests as:
     ```text
     InvalidOperationException: A second operation was started on this context instance before a previous operation completed.
     This is usually caused by different threads concurrently using the same instance of DbContext.
     ```

### 2. The Solution: Method Injection in `InvokeAsync`
Move the scoped service parameter to `InvokeAsync`:
```csharp
public class ScopedServiceExperimentMiddleware
{
    private readonly RequestDelegate _next;

    public ScopedServiceExperimentMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    // RESOLVED PER REQUEST:
    public async Task InvokeAsync(HttpContext context, IScopedStateService scopedService)
    {
        // scopedService is retrieved directly from context.RequestServices (current request's IServiceScope)
        ...
    }
}
```

#### Observed Concurrent Results:
```bash
# Terminal 1:
curl -s "http://localhost:5187/api/diagnostics/concurrency-test?client=Terminal-A"
# Output:
{"client":"Terminal-A","result":"[ISOLATED SCOPE] InstanceId: 50699812-541c-4700-817d-3e208590260c, ClientTag: 'Terminal-A', Concurrent Calls: 1","scopedInstanceId":"50699812-541c-4700-817d-3e208590260c","resolvedVia":"InvokeAsync (Per-Request Scope)"}

# Terminal 2:
curl -s "http://localhost:5187/api/diagnostics/concurrency-test?client=Terminal-B"
# Output:
{"client":"Terminal-B","result":"[ISOLATED SCOPE] InstanceId: 04fe086b-6e97-43a4-b90b-5937ac2a6b2e, ClientTag: 'Terminal-B', Concurrent Calls: 1","scopedInstanceId":"04fe086b-6e97-43a4-b90b-5937ac2a6b2e","resolvedVia":"InvokeAsync (Per-Request Scope)"}
```
* **Terminal A Instance:** `50699812-541c-4700-817d-3e208590260c`
* **Terminal B Instance:** `04fe086b-6e97-43a4-b90b-5937ac2a6b2e`
Each request receives its own isolated instance. Concurrency races and cross-client data leakage are eliminated.
