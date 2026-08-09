
namespace Kafka.StateMachines;

partial class StateMachinesFuncs
{
  static readonly HashSet<string> ConsumingCriticalStates =
  [
    CapturingStates.CaptureKafkaMessageCriticalErrorState,
    OffsettingStates.OffsetConsumeCriticalErrorState,
    Operations.Inbox.DispatchingStates.DispatchDeadLetterCriticalErrorState
  ];
}