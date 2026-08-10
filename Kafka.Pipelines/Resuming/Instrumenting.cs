using static Kafka.Pipelines.ResumingCounters;

namespace Kafka.Pipelines;

partial class PipelinesFuncs
{
  [LoggerMessage(32, LogLevel.Information, "Resumed inbox message. MessageId: {messageId}. State: {state}")]
  static partial void LogResumedInboxMessage(ILogger logger, Guid? messageId, string state);

  [LoggerMessage(33, LogLevel.Error, "Resume inbox messages critical error. State: {state}")]
  static partial void LogResumeInboxMessagesCriticalError(ILogger logger, string state);

  [LoggerMessage(34, LogLevel.Error, "Fetch inbox messages error.")]
  static partial void LogFetchInboxMessagesError(ILogger logger, Exception exception);

  internal static void InstrumentResumedInboxMessage(
    Guid? messageId,
    string state,
    IInstrumentationServices services)
  {
    LogResumedInboxMessage(services.GetLogger(), messageId, state);
    AddMetricCounter(ResumedCounter);
  }

  static void InstrumentResumeInboxMessageCriticalError(
    string state,
    IInstrumentationServices services)
  {
    LogResumeInboxMessagesCriticalError(services.GetLogger(), state);
    AddMetricCounter(ResumeCriticalErrorsCounter);
  }

  internal static void InstrumentFetchInboxMessageError(
    Exception exception,
    IInstrumentationServices services)
  {
    LogFetchInboxMessagesError(services.GetLogger(), exception);
    AddMetricCounter(FetchErrorCounter);
  }
}
