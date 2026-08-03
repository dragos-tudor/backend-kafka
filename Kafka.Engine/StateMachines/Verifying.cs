
namespace Kafka.Engine;

partial class EngineFuncs
{
  static bool IsTerminalState<TState>(
    IReadOnlySet<TState> terminalStates,
    TState state) =>
      terminalStates.Contains(state);
}