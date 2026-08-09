
namespace Kafka.StateMachines;

static class ResumingStates
{
  internal const string ResumingNotStartedState = "Resuming inbox messages not started";
  internal const string ResumingCriticalErrorState = "Resuming inbox messages critical error.";

  internal static string GetResumingEntryState(InboxMessageStatus status) =>
    status switch
    {
      InboxMessageStatus.Pending => ResumingNotStartedState,
      InboxMessageStatus.DeadLettering => Operations.Inbox.SchedulingStates.ScheduleInboxMessageExhaustedState,
      _ => throw new InvalidOperationException($"Invalid resuming inbox message status: {status}.")
    };
}
