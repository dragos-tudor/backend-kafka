
using System.Runtime.CompilerServices;

namespace Kafka.Pipelines;

partial class PipelinesFuncs
{
  internal static async IAsyncEnumerable<(TData, TState)> RunStateMachineAsync<TServices, TData, TState>(
    TServices services,
    TData initialData,
    TState initialState,
    Func<TState, StepAsync<TServices, TData, TState>?> getStateAction,
    [EnumeratorCancellation] CancellationToken ct = default)
  {
    var currentState = initialState;
    var currentData = initialData;
    while (getStateAction(currentState) is StepAsync<TServices, TData, TState> action &&
          !ct.IsCancellationRequested)
    {
      var (newData, newState) = await action(services, currentData, ct);

      currentData = newData;
      currentState = newState;
      yield return (currentData, currentState);
    }
    yield return (currentData, currentState);
  }
}