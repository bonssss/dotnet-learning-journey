namespace AddsEats.Api.Services.Diagnostics;

public interface IScopedStateService
{
    Guid InstanceId { get; }
    Task<string> ProcessAsync(string clientTag);
}

public class ScopedStateService : IScopedStateService
{
    public Guid InstanceId { get; } = Guid.NewGuid();
    private string? _capturedClientTag;
    private int _concurrencyCounter = 0;

    public async Task<string> ProcessAsync(string clientTag)
    {
        _capturedClientTag = clientTag;
        var concurrentCalls = Interlocked.Increment(ref _concurrencyCounter);

        // Simulate async I/O work while holding internal per-request state
        await Task.Delay(80);

        string outcome;
        if (concurrentCalls > 1 || _capturedClientTag != clientTag)
        {
            outcome = $"[CONCURRENCY BUG - CAPTIVE DEPENDENCY] InstanceId: {InstanceId}, Expected '{clientTag}', but Overwritten by '{_capturedClientTag}', Concurrent Calls: {concurrentCalls}";
        }
        else
        {
            outcome = $"[ISOLATED SCOPE] InstanceId: {InstanceId}, ClientTag: '{_capturedClientTag}', Concurrent Calls: {concurrentCalls}";
        }

        Interlocked.Decrement(ref _concurrencyCounter);
        return outcome;
    }
}
