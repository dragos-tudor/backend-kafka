
namespace Kafka.StateMachines;

partial class StateMachinesFuncs
{
  static readonly HashSet<string> ResumingCriticalStates = [
    Operations.Inbox.DispatchingStates.DispatchDeadLetterErrorState
  ];
}