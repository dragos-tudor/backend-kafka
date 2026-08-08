using static Kafka.Operations.Outbox.OutboxCounterType;

namespace Kafka.Operations.Outbox;

partial class OutboxFuncs
{
  [LoggerMessage(15, LogLevel.Information, "Schedule outbox message retry. MessageId: {messageId}. RetryCount: {retryCount}. Error: {error}")]
  static partial void LogScheduleOutboxMessageRetry(ILogger logger, Guid? messageId, int retryCount, string error);

  [LoggerMessage(16, LogLevel.Information, "Schedule outbox message exhausted. MessageId: {messageId}. RetryCount: {retryCount}. Error: {error}")]
  static partial void LogScheduleOutboxMessageExhausted(ILogger logger, Guid? messageId, int retryCount, string error);

  [LoggerMessage(17, LogLevel.Error, "Schedule outbox message error. MessageId: {messageId}.")]
  static partial void LogScheduleOutboxMessageError(ILogger logger, Guid? messageId, Exception ex);

  static Activity? InstrumentScheduleOutboxMessageRetry(
    Guid? messageId,
    int retryCount,
    string error,
    IInstrumentationServices services)
  {
    LogScheduleOutboxMessageRetry(services.GetLogger(), messageId, retryCount, error);
    AddMetricCounter(services.GetMetricCounters<OutboxCounterType>(), ScheduleOutboxRetryCounter);
    AddActivityTag(Activity.Current, "schedule.outbox.retryCount", retryCount);
    AddActivityTag(Activity.Current, "schedule.outbox.error", error);
    AddActivityEvent(Activity.Current, "schedule.outbox.retry",
      [CreateActivityEventAttribute("schedule.error", error)]);
    return Activity.Current;
  }

  static Activity? InstrumentScheduleOutboxMessageExhausted(
    Guid? messageId,
    int retryCount,
    string error,
    IInstrumentationServices services)
  {
    LogScheduleOutboxMessageExhausted(services.GetLogger(), messageId, retryCount, error);
    AddMetricCounter(services.GetMetricCounters<OutboxCounterType>(), ScheduleOutboxExhaustedCounter);
    AddActivityTag(Activity.Current, "schedule.outbox.retryCount", retryCount);
    AddActivityEvent(Activity.Current, "schedule.outbox.exhausted",
      [CreateActivityEventAttribute("schedule.error", error)]);
    return Activity.Current;
  }

  static Activity? InstrumentScheduleOutboxMessageError(
    Guid? messageId,
    Exception ex,
    IInstrumentationServices services)
  {
    LogScheduleOutboxMessageError(services.GetLogger(), messageId, ex);
    AddMetricCounter(services.GetMetricCounters<OutboxCounterType>(), ScheduleOutboxErrorCounter);
    AddActivityTag(Activity.Current, "schedule.outbox.error", ex);
    AddActivityEvent(Activity.Current, "schedule.outbox.error",
      [CreateActivityEventAttribute("schedule.error", ex)]);
    return Activity.Current;
  }
}