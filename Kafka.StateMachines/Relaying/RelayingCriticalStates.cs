
namespace Kafka.StateMachines;

partial class StateMachinesFuncs
{
  static readonly HashSet<string> RelayingCriticalStates =
  [
    PublishingStates.PublishOutboxMessageErrorState,
    Operations.Outbox.DispatchingStates.DispatchDeadLetterErrorState
  ];
}