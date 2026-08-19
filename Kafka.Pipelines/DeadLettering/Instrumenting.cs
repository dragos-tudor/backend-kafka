using static Kafka.Pipelines.DeadLetteringCounters;

namespace Kafka.Pipelines;

partial class PipelinesFuncs
{
  [LoggerMessage(40, LogLevel.Information, "Dead-lettered inbox message. MessageId: {messageId}. State: {state}")]
  static partial void LogDeadLetteredInboxMessage(ILogger logger, Guid? messageId, string state);

  [LoggerMessage(41, LogLevel.Error, "Fetch dead-lettering inbox messages error.")]
  static partial void LogFetchDeadLetteringInboxMessagesError(ILogger logger, Exception exception);

  internal static void InstrumentDeadLetteredInboxMessage(
    Guid? messageId,
    string state,
    IInstrumentationServices services)
  {
    LogDeadLetteredInboxMessage(services.GetLogger(), messageId, state);
    AddMetricCounter(DeadLetteredCounter);
  }

  internal static void InstrumentFetchDeadLetteringInboxMessageError(
    Exception exception,
    IInstrumentationServices services)
  {
    LogFetchDeadLetteringInboxMessagesError(services.GetLogger(), exception);
    AddMetricCounter(FetchErrorCounter);
  }
}
