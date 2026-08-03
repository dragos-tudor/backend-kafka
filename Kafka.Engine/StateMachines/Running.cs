
namespace Kafka.Engine;

partial class EngineFuncs
{
  internal static async IAsyncEnumerable<TState> RunStateMachineAsync<TServices, TData, TState>(
    StepContext<TServices, TData> ctx,
    IDictionary<TState, StepAsync<TServices, TData, TState>> stateActions,
    IReadOnlySet<TState> terminalStates,
    TState initialState,
    [EnumeratorCancellation] CancellationToken ct = default)
  {
    var state = initialState;
    while (!IsTerminalState(terminalStates, state))
    {
      var action = stateActions[state];
      state = await action(ctx, ct);
      yield return state;
    }
  }
}