using static Kafka.Pipelines.RedeliveringCounters;

namespace Kafka.Pipelines;

partial class PipelinesFuncs
{
  [LoggerMessage(32, LogLevel.Information, "Redelivered dead letter message. MessageId: {messageId}. State: {state}")]
  static partial void LogRedeliveredDeadLetterMessage(ILogger logger, Guid? messageId, string state);

  [LoggerMessage(33, LogLevel.Error, "Redeliver dead letter messages critical error. State: {state}")]
  static partial void LogRedeliverDeadLetterMessageCriticalError(ILogger logger, string state);

  [LoggerMessage(34, LogLevel.Error, "Fetch dead letter messages error.")]
  static partial void LogFetchDeadLetterMessagesError(ILogger logger, Exception exception);

  internal static void InstrumentRedeliveredDeadLetterMessage(
    Guid? messageId,
    string state,
    IInstrumentationServices services)
  {
    LogRedeliveredDeadLetterMessage(services.GetLogger(), messageId, state);
    AddMetricCounter(RedeliveredCounter);
  }

  static void InstrumentRedeliverDeadLetterMessageCriticalError(
    string state,
    IInstrumentationServices services)
  {
    LogRedeliverDeadLetterMessageCriticalError(services.GetLogger(), state);
    AddMetricCounter(RedeliverCriticalErrorsCounter);
  }

  internal static void InstrumentFetchDeadLetterMessageError(
    Exception exception,
    IInstrumentationServices services)
  {
    LogFetchDeadLetterMessagesError(services.GetLogger(), exception);
    AddMetricCounter(FetchErrorCounter);
  }
}
