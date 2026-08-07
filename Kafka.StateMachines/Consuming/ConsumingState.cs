
namespace Kafka.StateMachines;

partial class StateMachinesFuncs
{
  const string NotStartedConsumeState = "Consume kafka messages not started";
  const string CriticalErrorConsumeState = "Consume kafka messages critical error.";

  static readonly HashSet<string> ConsumingCriticalStates = [
    NotStartedConsumeState,
    CaptureKafkaMessageErrorState,
    InsertInboxMessageErrorState,
    OffsetConsumeErrorState
  ];
}
