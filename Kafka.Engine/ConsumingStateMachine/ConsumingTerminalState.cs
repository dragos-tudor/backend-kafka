using static Kafka.Engine.ConsumingState;

namespace Kafka.Engine;

partial class EngineFuncs
{
  static HashSet<ConsumingState> GetConsumingTerminalStates() =>
    new HashSet<ConsumingState>
    {
      NotCapturedKafkaMessageState,
      AlreadySavedInboxMessageState,
    };
}
