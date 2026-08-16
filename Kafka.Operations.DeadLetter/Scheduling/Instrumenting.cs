using static Kafka.Operations.DeadLetter.SchedulingCounters;

namespace Kafka.Operations.DeadLetter;

partial class DeadLetterFuncs
{
  [LoggerMessage(15, LogLevel.Information, "Schedule dead tetter message retry. MessageId: {messageId}. RetryCount: {retryCount}. Error: {error}")]
  static partial void LogScheduleDeadLetterMessageRetry(ILogger logger, Guid? messageId, int retryCount, string? error);

  [LoggerMessage(16, LogLevel.Information, "Schedule dead tetter message exhausted. MessageId: {messageId}. RetryCount: {retryCount}. Error: {error}")]
  static partial void LogScheduleDeadLetterMessageExhausted(ILogger logger, Guid? messageId, int retryCount, string? error);

  [LoggerMessage(17, LogLevel.Error, "Schedule dead letter message error. MessageId: {messageId}.")]
  static partial void LogScheduleDeadLetterMessageError(ILogger logger, Guid? messageId, Exception ex);

  static Activity? InstrumentScheduleDeadLetterMessageRetry(
    Guid? messageId,
    int retryCount,
    string? error,
    IInstrumentationServices services)
  {
    LogScheduleDeadLetterMessageRetry(services.GetLogger(), messageId, retryCount, error);
    AddMetricCounter(ScheduleDeadLetterRetryCounter);
    AddActivityTag(Activity.Current, "schedule.deadletter.retryCount", retryCount);
    AddActivityTag(Activity.Current, "schedule.deadletter.error", error);
    AddActivityEvent(Activity.Current, "schedule.deadletter.retry",
      [CreateActivityEventAttribute("schedule.error", error)]);
    return Activity.Current;
  }

  static Activity? InstrumentScheduleDeadLetterMessageExhausted(
    Guid? messageId,
    int retryCount,
    string? error,
    IInstrumentationServices services)
  {
    LogScheduleDeadLetterMessageExhausted(services.GetLogger(), messageId, retryCount, error);
    AddMetricCounter(ScheduleDeadLetterExhaustedCounter);
    AddActivityTag(Activity.Current, "schedule.deadletter.retryCount", retryCount);
    AddActivityEvent(Activity.Current, "schedule.deadletter.exhausted",
      [CreateActivityEventAttribute("schedule.error", error)]);
    return Activity.Current;
  }

  static Activity? InstrumentScheduleDeadLetterMessageError(
    Guid? messageId,
    Exception ex,
    IInstrumentationServices services)
  {
    LogScheduleDeadLetterMessageError(services.GetLogger(), messageId, ex);
    AddMetricCounter(ScheduleDeadLetterErrorCounter);
    AddActivityTag(Activity.Current, "schedule.deadletter.error", ex);
    AddActivityEvent(Activity.Current, "schedule.deadletter.error",
      [CreateActivityEventAttribute("schedule.error", ex)]);
    return Activity.Current;
  }
}