
namespace Kafka.Operations.Outbox;

partial class OutboxFuncs
{
  internal const string ScheduleOutboxMessageExhaustedState = "ScheduleOutboxMessageExhaustedState";
  internal const string ScheduleOutboxMessageRetryState = "ScheduleOutboxMessageRetryState";
  internal const string ScheduleOutboxMessageErrorState = "ScheduleOutboxMessageErrorState";

  const int MaxScheduleRetries = 5;

  internal static string GetScheduleOutboxMessageState(
    int currentRetryCount,
    int maxScheduleRetries = MaxScheduleRetries) =>
      currentRetryCount + 1 < maxScheduleRetries
          ? ScheduleOutboxMessageRetryState
          : ScheduleOutboxMessageExhaustedState;

  internal static OutboxMessageStatus GetScheduleOutboxMessageStatus(
    string state) =>
      state switch
      {
        ScheduleOutboxMessageExhaustedState => OutboxMessageStatus.DeadLettering,
        ScheduleOutboxMessageRetryState => OutboxMessageStatus.Pending,
        _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
      };
}