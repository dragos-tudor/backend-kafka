
namespace Kafka.StateMachines;

partial class StateMachinesFuncs
{
  const string ConsumingNotStartedState = "Consuming kafka messages not started";
  const string ConsumingCriticalErrorState = "Consuming kafka messages critical error.";

  static readonly HashSet<string> ConsumingCriticalStates =
  [
    CaptureKafkaMessageErrorState,
    InsertInboxMessageErrorState,
    OffsetConsumeErrorState
  ];
}
