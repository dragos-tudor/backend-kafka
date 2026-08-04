using static Kafka.Operations.MetricCounterType;

namespace Kafka.Operations;

partial class OperationsFuncs
{
  [LoggerMessage(7, LogLevel.Debug, "Published dead letter. MessageId: {messageId}. DeadLetterKey: {deadLetterKey}. DeadLetterTopic: {deadLetterTopic}. Domain error: {domainError}")]
  static partial void LogPublishedDeadLetter(ILogger logger, Guid? messageId, string? deadLetterKey, string deadLetterTopic, string? domainError);

  static void InstrumentPublishDeadLetter(
    Guid? messageId,
    string? deadLetterKey,
    string deadLetterTopic,
    string? domainError,
    IInstrumentationServices services)
  {
    LogPublishedDeadLetter(services.GetLogger(), messageId, deadLetterKey, deadLetterTopic, domainError);
    AddMetricCounter(services.GetMetricCounters(), DeadLetteredCounter);
    AddActivityTag(Activity.Current, "deadletter.key", deadLetterKey);
    AddActivityTag(Activity.Current, "deadletter.topic", deadLetterTopic);
    AddActivityTag(Activity.Current, "deadletter.reason", domainError);
    AddActivityEvent(Activity.Current, "deadletter.published");
  }
}