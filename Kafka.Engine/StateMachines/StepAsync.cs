
namespace Kafka.Engine;

public delegate ValueTask<TState> StepAsync<TServices, TData, TState> (StepContext<TServices, TData> ctx, CancellationToken ct);