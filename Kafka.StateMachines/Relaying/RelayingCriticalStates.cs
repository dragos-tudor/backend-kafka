
namespace Kafka.StateMachines;

partial class StateMachinesFuncs
{
  static readonly HashSet<string> RelayingCriticalStates = [
    PublishingStates.PublishOutboxMessageCriticalErrorState,
    Operations.Outbox.DispatchingStates.DispatchDeadLetterCriticalErrorState
  ];
}