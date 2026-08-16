using static Kafka.Operations.Inbox.CapturingCounters;

namespace Kafka.Operations.Inbox;

partial class InboxFuncs
{
  [LoggerMessage(1, LogLevel.Information, "Captured kafka message. MessageKey: {messageKey}, Offset: {topicOffsetPartition}")]
  static partial void LogCapturedKafkaMessage(ILogger logger, string? messageKey, TopicPartitionOffset topicOffsetPartition);

  [LoggerMessage(2, LogLevel.Error, "Capture kafka message error.")]
  static partial void LogCaptureKafkaMessageError(ILogger logger, Exception exception);

  [LoggerMessage(3, LogLevel.Error, "Capture kafka message critical error.")]
  static partial void LogCaptureKafkaMessageCriticalError(ILogger logger, Exception exception);

  [LoggerMessage(4, LogLevel.Information, "Not captured kafka message.")]
  static partial void LogNotCapturedKafkaMessage(ILogger logger);

  static void InstrumentCapturedKafkaMessage(
    string? messageKey,
    TopicPartitionOffset topicOffsetPartition,
    string? traceParent,
    IInstrumentationServices services)
  {
    LogCapturedKafkaMessage(services.GetLogger(), messageKey, topicOffsetPartition);
    var activityContext = ToActivityContext(traceParent);
    if (activityContext is not null)
      SetActivityParentId(Activity.Current, activityContext.Value);
    AddActivityTag(Activity.Current, "capture.message.key", messageKey);
    AddActivityTag(Activity.Current, "capture.topic", topicOffsetPartition.Topic);
    AddActivityTag(Activity.Current, "capture.partition", topicOffsetPartition.Partition.Value);
    AddActivityTag(Activity.Current, "capture.offset", topicOffsetPartition.Offset);
    AddMetricCounter(CapturedCounter);
    AddActivityEvent(Activity.Current, "captured.message");
  }

  static void InstrumentCaptureKafkaMessageError(
    Exception ex,
    IInstrumentationServices services)
  {
    LogCaptureKafkaMessageError(services.GetLogger(), ex);
    AddMetricCounter(CaptureErrorCounter);
    AddActivityEvent(Activity.Current, "capture.message.error");
  }

  static void InstrumentCaptureKafkaMessageCriticalError(
    Exception ex,
    IInstrumentationServices services)
  {
    LogCaptureKafkaMessageCriticalError(services.GetLogger(), ex);
    AddMetricCounter(CaptureCriticalErrorCounter);
    AddActivityEvent(Activity.Current, "capture.message.critical.error");
  }

  static void InstrumentNotCapturedKafkaMessage(
    IInstrumentationServices services)
  {
    LogNotCapturedKafkaMessage(services.GetLogger());
    AddMetricCounter(NotCapturedCounter);
    AddActivityEvent(Activity.Current, "not.captured.message");
  }

}