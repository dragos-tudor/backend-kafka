
namespace Kafka.StateMachines;

public delegate ValueTask<(TData, TState)> StepAsync<TServices, TData, TState> (TServices services, TData data, CancellationToken ct);