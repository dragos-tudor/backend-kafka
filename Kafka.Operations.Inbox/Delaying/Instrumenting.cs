using static Kafka.Operations.Inbox.DelayingCounters;

namespace Kafka.Operations.Inbox;

partial class InboxFuncs
{
  [LoggerMessage(20, LogLevel.Information, "Delay inbox message retry. MessageId: {messageId}. RetryCount: {retryCount}. Error: {error}")]
  static partial void LogDelayInboxMessageRetry(ILogger logger, Guid? messageId, int retryCount, string error);

  [LoggerMessage(21, LogLevel.Information, "Delay inbox message exhausted. MessageId: {messageId}. RetryCount: {retryCount}. Error: {error}")]
  static partial void LogDelayInboxMessageExhausted(ILogger logger, Guid? messageId, int retryCount, string error);

  [LoggerMessage(22, LogLevel.Error, "Delay inbox message error. MessageId: {messageId}.")]
  static partial void LogDelayInboxMessageError(ILogger logger, Guid? messageId, Exception ex);

  static Activity? InstrumentDelayDeadLetterRetry(
    Guid? messageId,
    int retryCount,
    string error,
    IInstrumentationServices services)
  {
    LogDelayInboxMessageRetry(services.GetLogger(), messageId, retryCount, error);
    AddMetricCounter(DelayDeadLetterRetryCounter);
    AddActivityTag(Activity.Current, "delay.inbox.retryCount", retryCount);
    AddActivityTag(Activity.Current, "delay.inbox.error", error);
    AddActivityEvent(Activity.Current, "delay.inbox.retry",
      [CreateActivityEventAttribute("delay.error", error)]);
    return Activity.Current;
  }

  static Activity? InstrumentDelayDeadLetterExhausted(
    Guid? messageId,
    int retryCount,
    string error,
    IInstrumentationServices services)
  {
    LogDelayInboxMessageExhausted(services.GetLogger(), messageId, retryCount, error);
    AddMetricCounter(DelayDeadLetterExhaustedCounter);
    AddActivityTag(Activity.Current, "delay.inbox.retryCount", retryCount);
    AddActivityEvent(Activity.Current, "delay.inbox.exhausted",
      [CreateActivityEventAttribute("delay.error", error)]);
    return Activity.Current;
  }

  static Activity? InstrumentDelayDeadLetterError(
    Guid? messageId,
    Exception ex,
    IInstrumentationServices services)
  {
    LogDelayInboxMessageError(services.GetLogger(), messageId, ex);
    AddMetricCounter(DelayDeadLetterErrorCounter);
    AddActivityTag(Activity.Current, "delay.inbox.error", ex);
    AddActivityEvent(Activity.Current, "delay.inbox.error",
      [CreateActivityEventAttribute("delay.error", ex)]);
    return Activity.Current;
  }
}