using static Kafka.Inbox.InboxCounterType;

namespace Kafka.Inbox;

partial class InboxFuncs
{
  [LoggerMessage(15, LogLevel.Information, "Schedule inbox message retry. MessageId: {messageId}. RetryCount: {retryCount}. Error: {error}")]
  static partial void LogScheduleInboxMessageRetry(ILogger logger, Guid? messageId, int retryCount, string error);

  [LoggerMessage(16, LogLevel.Information, "Schedule inbox message exhausted. MessageId: {messageId}. RetryCount: {retryCount}. Error: {error}")]
  static partial void LogScheduleInboxMessageExhausted(ILogger logger, Guid? messageId, int retryCount, string error);

  [LoggerMessage(17, LogLevel.Error, "Schedule inbox message error. MessageId: {messageId}.")]
  static partial void LogScheduleInboxMessageError(ILogger logger, Guid? messageId, Exception ex);

  static Activity? InstrumentScheduleInboxMessageRetry(
    Guid? messageId,
    int retryCount,
    string error,
    IInstrumentationServices services)
  {
    LogScheduleInboxMessageRetry(services.GetLogger(), messageId, retryCount, error);
    AddMetricCounter(services.GetMetricCounters<InboxCounterType>(), ScheduleInboxRetryCounter);
    AddActivityTag(Activity.Current, "schedule.retryCount", retryCount);
    AddActivityTag(Activity.Current, "schedule.error", error);
    AddActivityEvent(Activity.Current, "schedule.inbox.retry",
      [CreateActivityEventAttribute("schedule.error", error)]);
    return Activity.Current;
  }

  static Activity? InstrumentScheduleInboxMessageExhausted(
    Guid? messageId,
    int retryCount,
    string error,
    IInstrumentationServices services)
  {
    LogScheduleInboxMessageExhausted(services.GetLogger(), messageId, retryCount, error);
    AddMetricCounter(services.GetMetricCounters<InboxCounterType>(), ScheduleInboxExhaustedCounter);
    AddActivityTag(Activity.Current, "schedule.retryCount", retryCount);
    AddActivityEvent(Activity.Current, "schedule.inbox.exhausted",
      [CreateActivityEventAttribute("schedule.error", error)]);
    return Activity.Current;
  }

  static Activity? InstrumentScheduleInboxMessageError(
    Guid? messageId,
    Exception ex,
    IInstrumentationServices services)
  {
    LogScheduleInboxMessageError(services.GetLogger(), messageId, ex);
    AddMetricCounter(services.GetMetricCounters<InboxCounterType>(), ScheduleInboxErrorCounter);
    AddActivityTag(Activity.Current, "schedule.error", ex);
    AddActivityEvent(Activity.Current, "schedule.inbox.error",
      [CreateActivityEventAttribute("schedule.error", ex)]);
    return Activity.Current;
  }
}