
namespace Kafka.Pipelines;

static class RelayingStates
{
  internal const string RelayingNotStartedState = "Relaying outbox messages not started";
  internal const string RelayingCriticalErrorState = "Relaying outbox messages critical error.";

  internal static string GetRelayingEntryState(OutboxMessageStatus status) =>
    status switch
    {
      OutboxMessageStatus.Pending => RelayingNotStartedState,
      OutboxMessageStatus.Dispatching => Operations.Outbox.SchedulingStates.ScheduleOutboxMessageExhaustedState,
      _ => throw new InvalidOperationException($"Invalid relaying outbox message status: {status}.")
    };
}
