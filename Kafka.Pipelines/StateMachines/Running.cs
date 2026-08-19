
namespace Kafka.Pipelines;

partial class PipelinesFuncs
{
  internal static async Task<(TData, TState)> RunStateMachineAsync<TServices, TData, TState>(
    TServices services,
    TData data,
    TState state,
    Func<TState, StepAsync<TServices, TData, TState>?> getStateAction,
    CancellationToken ct = default)
  {
    if (ct.IsCancellationRequested) return (data, state);

    var stateAction = getStateAction(state);
    if (stateAction is null) return (data, state);

    var (newData, newState) = await stateAction(services, data, ct);
    if (state is null || state.Equals(newState)) return (newData, newState);

    return await RunStateMachineAsync(services, newData, newState, getStateAction, ct);
  }
}