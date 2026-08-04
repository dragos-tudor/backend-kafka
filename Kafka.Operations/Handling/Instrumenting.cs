using static Kafka.Operations.MetricCounterType;

namespace Kafka.Operations;

partial class OperationsFuncs
{
  [LoggerMessage(5, LogLevel.Debug, "Handled inbox message.")]
  static partial void LogHandledInboxMessage(ILogger logger);

  [LoggerMessage(6, LogLevel.Error, "Handled inbox message failed. Domain error: {domainError}")]
  static partial void LogHandledInboxMessageFailed(ILogger logger, string domainError);

  static Activity? InstrumentHandleInboxMessage(
    IInstrumentationServices services)
  {
    LogHandledInboxMessage(services.GetLogger());
    AddMetricCounter(services.GetMetricCounters(), HandledCounter);
    AddActivityEvent(Activity.Current, "message.handled");
    return Activity.Current;
  }

  private static Activity? InstrumentHandleInboxMessageError(
    string domainError,
    IInstrumentationServices services)
  {
    LogHandledInboxMessageFailed(services.GetLogger(), domainError);
    AddActivityTag(Activity.Current, "domain.error", domainError);
    AddActivityEvent(Activity.Current, "message.handling.failed",
      [CreateActivityEventAttribute("domain.error", domainError)]);
    return Activity.Current;
  }
}