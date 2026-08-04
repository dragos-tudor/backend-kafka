using static Kafka.Operations.MetricCounterType;

namespace Kafka.Operations;

partial class OperationsFuncs
{
  [LoggerMessage(1, LogLevel.Information, "Captured kafka message. MessageId: {messageId}, CorrelationId: {correlationId}, Offset: {topicOffsetPartition}")]
  static partial void LogCapturedKafkaMessage(ILogger logger, Guid? messageId, Guid? correlationId, TopicPartitionOffset topicOffsetPartition);

  static void InstrumentCaptureKafkaMessage(
    Guid? messageId,
    Guid? correlationId,
    TopicPartitionOffset topicOffsetPartition,
    IInstrumentationServices services)
  {
    LogCapturedKafkaMessage(services.GetLogger(), messageId, correlationId, topicOffsetPartition);
    SetCapturingActivityTags(messageId, correlationId, topicOffsetPartition);
    AddMetricCounter(services.GetMetricCounters(), CapturedCounter);
    AddActivityEvent(Activity.Current, "message.captured");
  }

  static Activity? SetCapturingActivityTags(
   Guid? messageId,
   Guid? correlationId,
   TopicPartitionOffset topicOffsetPartition) =>
     Activity.Current?
       .AddTag(ActivityTagNames.KafkaTopic, topicOffsetPartition.Topic)
       .AddTag(ActivityTagNames.KafkaPartition, topicOffsetPartition.Partition.Value)
       .AddTag(ActivityTagNames.KafkaOffset, topicOffsetPartition.Offset)
       .AddTag(ActivityTagNames.MessageId, messageId)
       .AddTag(ActivityTagNames.CorrelationId, correlationId);
}