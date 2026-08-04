using static Kafka.Operations.MetricCounterType;

namespace Kafka.Operations;

partial class OperationsFuncs
{
  [LoggerMessage(5, LogLevel.Debug, "Handled inbox message. MessageId: {messageId}")]
  static partial void LogHandledInboxMessage(ILogger logger, Guid? messageId);

  [LoggerMessage(6, LogLevel.Error, "Handling inbox message failed. MessageId: {messageId}. Domain error: {domainError}")]
  static partial void LogHandlingInboxMessageFailed(ILogger logger, Guid? messageId, string domainError);

  static Activity? InstrumentHandleInboxMessage(
    Guid? messageId,
    IInstrumentationServices services)
  {
    LogHandledInboxMessage(services.GetLogger(), messageId);
    AddMetricCounter(services.GetMetricCounters(), HandledCounter);
    AddActivityEvent(Activity.Current, "message.handled");
    return Activity.Current;
  }

  private static Activity? InstrumentHandleInboxMessageError(
    Guid? messageId,
    string domainError,
    IInstrumentationServices services)
  {
    LogHandlingInboxMessageFailed(services.GetLogger(), messageId, domainError);
    AddActivityTag(Activity.Current, "domain.error", domainError);
    AddActivityEvent(Activity.Current, "message.handling.failed",
      [CreateActivityEventAttribute("domain.error", domainError)]);
    return Activity.Current;
  }
}