using static Kafka.Operations.Inbox.InboxCounterType;

namespace Kafka.Operations.Inbox;

partial class InboxFuncs
{
  [LoggerMessage(18, LogLevel.Information, "Dispatched dead letter. MessageId: {messageId}. DeadLetterKey: {deadLetterKey}. DeadLetterTopic: {deadLetterTopic}. Handle error: {handleError}")]
  static partial void LogDispatchedDeadLetter(ILogger logger, Guid? messageId, string? deadLetterKey, string deadLetterTopic, string? handleError);

  [LoggerMessage(19, LogLevel.Error, "Dispatch dead letter error. MessageId: {messageId}.")]
  static partial void LogDispatchDeadLetterError(ILogger logger, Guid? messageId, Exception exception);

  static void InstrumentDispatchedDeadLetter(
    Guid? messageId,
    string? deadLetterKey,
    string deadLetterTopic,
    string? error,
    IInstrumentationServices services)
  {
    LogDispatchedDeadLetter(services.GetLogger(), messageId, deadLetterKey, deadLetterTopic, error);
    AddMetricCounter(services.GetMetricCounters<InboxCounterType>(), DispatchedDeadLetterCounter);
    AddActivityTag(Activity.Current, "dispatch.deadletter.key", deadLetterKey);
    AddActivityTag(Activity.Current, "dispatch.deadletter.topic", deadLetterTopic);
    AddActivityTag(Activity.Current, "dispatch.deadletter.reason", error);
    AddActivityEvent(Activity.Current, "dispatched.deadletter");
  }

  static void InstrumentDispatchDeadLetterError(
    Guid? messageId,
    Exception ex,
    IInstrumentationServices services)
  {
    LogDispatchDeadLetterError(services.GetLogger(), messageId, ex);
    AddMetricCounter(services.GetMetricCounters<InboxCounterType>(), DispatchDeadLetterErrorCounter);
    AddActivityEvent(Activity.Current, "dispatch.deadletter.error", [
      CreateActivityEventAttribute("dispatch.deadletter.error", ex),
    ]);
  }
}