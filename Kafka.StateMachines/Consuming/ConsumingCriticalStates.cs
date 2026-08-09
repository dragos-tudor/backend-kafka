
namespace Kafka.StateMachines;

partial class StateMachinesFuncs
{
  static readonly HashSet<string> ConsumingCriticalStates =
  [
    CapturingStates.CaptureKafkaMessageErrorState,
    InsertingStates.InsertInboxMessageErrorState,
    OffsettingStates.OffsetConsumeErrorState
  ];
}