
using System.Runtime.CompilerServices;

namespace Kafka.StateMachines;

partial class StateMachinesFuncs
{
  internal static async IAsyncEnumerable<(TData, TState)> RunStateMachineAsync<TServices, TData, TState>(
    TServices services,
    TData initialData,
    TState initialState,
    IImmutableDictionary<TState, StepAsync<TServices, TData, TState>> stateActions,
    [EnumeratorCancellation] CancellationToken ct = default)
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