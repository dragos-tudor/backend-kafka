
namespace Kafka.Operations.Inbox;

partial class InboxFuncs
{
  internal const string ScheduleInboxMessageExhaustedState = "ScheduleInboxMessageExhaustedState";
  internal const string ScheduleInboxMessageRetryState = "ScheduleInboxMessageRetryState";
  internal const string ScheduleInboxMessageErrorState = "ScheduleInboxMessageErrorState";

  const int MaxScheduleRetries = 5;

  internal static string GetScheduleInboxMessageState(
    int currentRetryCount,
    int maxScheduleRetries = MaxScheduleRetries) =>
      currentRetryCount + 1 < maxScheduleRetries
          ? ScheduleInboxMessageRetryState
          : ScheduleInboxMessageExhaustedState;

  internal static InboxMessageStatus GetScheduleInboxMessageStatus(
    string state) =>
      state switch
      {
        ScheduleInboxMessageExhaustedState => InboxMessageStatus.DeadLettering,
        ScheduleInboxMessageRetryState => InboxMessageStatus.Pending,
        _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
      };
}