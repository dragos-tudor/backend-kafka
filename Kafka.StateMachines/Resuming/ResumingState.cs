
namespace Kafka.StateMachines;

partial class StateMachinesFuncs
{
  const string ResumingNotStartedState = "Resuming inbox messages not started";
  const string ResumingCriticalErrorState = "Resuming inbox messages critical error.";

  static readonly HashSet<string> ResumingCriticalStates = [
    PublishOutboxMessageErrorState,
    InboxFuncs.DispatchDeadLetterErrorState
  ];

  static string GetResumingEntryState(InboxMessageStatus status) =>
    status switch
    {
      InboxMessageStatus.Pending => ResumingNotStartedState,
      InboxMessageStatus.DeadLettering => ScheduleInboxMessageExhaustedState,
      _ => throw new InvalidOperationException($"Invalid resuming inbox message status: {status}.")
    };
}
