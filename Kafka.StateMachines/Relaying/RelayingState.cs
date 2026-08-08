
namespace Kafka.StateMachines;

partial class StateMachinesFuncs
{
  const string RelayingNotStartedState = "Relaying outbox messages not started";
  const string RelayingCriticalErrorState = "Relaying outbox messages critical error.";

  static readonly HashSet<string> RelayingCriticalStates = new HashSet<string>
  {
    PublishOutboxMessageErrorState,
    OutboxFuncs.DispatchDeadLetterErrorState
  };

  static string GetRelayingEntryState(OutboxMessageStatus status) =>
    status switch
    {
      OutboxMessageStatus.Pending => RelayingNotStartedState,
      OutboxMessageStatus.DeadLettering => ScheduleOutboxMessageExhaustedState,
      _ => throw new InvalidOperationException($"Invalid relaying outbox message status: {status}.")
    };
}
