
namespace Kafka.StateMachines;

partial class StateMachinesFuncs
{
  const string NotStartedResumeState = "Resume inbox messages not started";
  const string CriticalErrorResumeState = "Resume inbox messages critical error.";

  static readonly HashSet<string> ResumingCriticalStates = [
    NotStartedResumeState,
    ScheduleInboxMessageErrorState,
    DelayDeadLetterErrorState
  ];

  static string GetResumingEntryState(InboxMessageStatus status) =>
    status switch
    {
      InboxMessageStatus.Pending => NotStartedResumeState,
      InboxMessageStatus.DeadLettering => ScheduleInboxMessageExhaustedState,
      _ => throw new InvalidOperationException($"Invalid resuming inbox message status: {status}.")
    };
}
