using static Kafka.Operations.Inbox.CapturingCounterType;

namespace Kafka.Operations.Inbox;

partial class InboxFuncs
{
  [LoggerMessage(1, LogLevel.Information, "Captured kafka message. MessageId: {messageId}, CorrelationId: {correlationId}, Offset: {topicOffsetPartition}")]
  static partial void LogCapturedKafkaMessage(ILogger logger, Guid? messageId, Guid? correlationId, TopicPartitionOffset topicOffsetPartition);

  [LoggerMessage(2, LogLevel.Error, "Capture kafka message error.")]
  static partial void LogCaptureKafkaMessageError(ILogger logger, Exception exception);

  [LoggerMessage(3, LogLevel.Information, "Not captured kafka message.")]
  static partial void LogNotCapturedKafkaMessageError(ILogger logger);

  static void InstrumentCapturedKafkaMessage(
    Guid? messageId,
    Guid? correlationId,
    TopicPartitionOffset topicOffsetPartition,
    IInstrumentationServices services)
  {
    LogCapturedKafkaMessage(services.GetLogger(), messageId, correlationId, topicOffsetPartition);
    AddActivityTag(Activity.Current, "capture.topic", topicOffsetPartition.Topic);
    AddActivityTag(Activity.Current, "capture.partition", topicOffsetPartition.Partition.Value);
    AddActivityTag(Activity.Current, "capture.offset", topicOffsetPartition.Offset);
    AddActivityTag(Activity.Current, "message.id", messageId);
    AddActivityTag(Activity.Current, "correlation.id", correlationId);
    AddMetricCounter(CapturingCounters[CapturedCounter]);
    AddActivityEvent(Activity.Current, "captured.message");
  }

  static void InstrumentCaptureKafkaMessageError(
    Exception ex,
    IInstrumentationServices services)
  {
    LogCaptureKafkaMessageError(services.GetLogger(), ex);
    AddMetricCounter(CapturingCounters[CaptureErrorCounter]);
    AddActivityEvent(Activity.Current, "capture.message.error");
  }

  static void InstrumentNotCapturedKafkaMessage(
    IInstrumentationServices services)
  {
    LogNotCapturedKafkaMessageError(services.GetLogger());
    AddMetricCounter(CapturingCounters[NotCapturedCounter]);
    AddActivityEvent(Activity.Current, "not.captured.message");
  }

}