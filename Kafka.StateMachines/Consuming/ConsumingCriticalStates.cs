
namespace Kafka.StateMachines;

partial class StateMachinesFuncs
{
  static readonly HashSet<string> ConsumingCriticalStates =
  [
    CapturingStates.CaptureKafkaMessageCrticalErrorState,
    OffsettingStates.OffsetConsumeCriticalErrorState,
    Operations.Inbox.DispatchingStates.DispatchDeadLetterCriticalErrorState
  ];
}