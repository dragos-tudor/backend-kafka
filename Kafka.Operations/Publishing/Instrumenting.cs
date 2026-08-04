using static Kafka.Operations.MetricCounterType;

namespace Kafka.Operations;

partial class OperationsFuncs
{
  [LoggerMessage(7, LogLevel.Debug, "Published dead letter.")]
  static partial void LogPublishedDeadLetter(ILogger logger);

  static void InstrumentPublishDeadLetter(
    string? domainError,
    string deadLetterTopic,
    IInstrumentationServices services)
  {
    LogPublishedDeadLetter(services.GetLogger());
    AddMetricCounter(services.GetMetricCounters(), DeadLetteredCounter);
    AddActivityTag(Activity.Current, "deadletter.topic", deadLetterTopic);
    AddActivityTag(Activity.Current, "deadletter.reason", domainError);
    AddActivityEvent(Activity.Current, "deadletter.published");
  }
}