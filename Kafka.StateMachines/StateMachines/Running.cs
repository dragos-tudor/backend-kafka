
using System.Runtime.CompilerServices;

namespace Kafka.StateMachines;

partial class StateMachinesFuncs
{
  internal static async IAsyncEnumerable<(TData, TState)> RunStateMachineAsync<TServices, TData, TState, TStateActions>(
    TServices services,
    TData initialData,
    TState initialState,
    TStateActions stateActions,
    [EnumeratorCancellation] CancellationToken ct = default)
  where TStateActions: IImmutableDictionary<TState, StepAsync<TServices, TData, TState>>
  {
    var currentState = initialState;
    var currentData = initialData;
    while (stateActions.ContainsKey(currentState))
    {
      var action = stateActions[currentState];
      var (newData, newState) = await action(services, currentData, ct);

      currentData = newData;
      currentState = newState;
      yield return (currentData, currentState);
    }
    yield return (currentData, currentState);
  }
}