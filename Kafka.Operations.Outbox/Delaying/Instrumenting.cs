using static Kafka.Operations.Outbox.DelayingCounterType;

namespace Kafka.Operations.Outbox;

partial class OutboxFuncs
{
  [LoggerMessage(20, LogLevel.Information, "Delay outbox message retry. MessageId: {messageId}. RetryCount: {retryCount}. Error: {error}")]
  static partial void LogDelayOutboxMessageRetry(ILogger logger, Guid? messageId, int retryCount, string error);

  [LoggerMessage(21, LogLevel.Information, "Delay outbox message exhausted. MessageId: {messageId}. RetryCount: {retryCount}. Error: {error}")]
  static partial void LogDelayOutboxMessageExhausted(ILogger logger, Guid? messageId, int retryCount, string error);

  [LoggerMessage(22, LogLevel.Error, "Delay outbox message error. MessageId: {messageId}.")]
  static partial void LogDelayOutboxMessageError(ILogger logger, Guid? messageId, Exception ex);

  static Activity? InstrumentDelayDeadLetterRetry(
    Guid? messageId,
    int retryCount,
    string error,
    IInstrumentationServices services)
  {
    LogDelayOutboxMessageRetry(services.GetLogger(), messageId, retryCount, error);
    AddMetricCounter(DelayingCounters[DelayDeadLetterRetryCounter]);
    AddActivityTag(Activity.Current, "delay.outbox.retryCount", retryCount);
    AddActivityTag(Activity.Current, "delay.outbox.error", error);
    AddActivityEvent(Activity.Current, "delay.outbox.retry",
      [CreateActivityEventAttribute("delay.error", error)]);
    return Activity.Current;
  }

  static Activity? InstrumentDelayDeadLetterExhausted(
    Guid? messageId,
    int retryCount,
    string error,
    IInstrumentationServices services)
  {
    LogDelayOutboxMessageExhausted(services.GetLogger(), messageId, retryCount, error);
    AddMetricCounter(DelayingCounters[DelayDeadLetterExhaustedCounter]);
    AddActivityTag(Activity.Current, "delay.outbox.retryCount", retryCount);
    AddActivityEvent(Activity.Current, "delay.outbox.exhausted",
      [CreateActivityEventAttribute("delay.error", error)]);
    return Activity.Current;
  }

  static Activity? InstrumentDelayDeadLetterError(
    Guid? messageId,
    Exception ex,
    IInstrumentationServices services)
  {
    LogDelayOutboxMessageError(services.GetLogger(), messageId, ex);
    AddMetricCounter(DelayingCounters[DelayDeadLetterErrorCounter]);
    AddActivityTag(Activity.Current, "delay.outbox.error", ex);
    AddActivityEvent(Activity.Current, "delay.outbox.error",
      [CreateActivityEventAttribute("delay.error", ex)]);
    return Activity.Current;
  }
}