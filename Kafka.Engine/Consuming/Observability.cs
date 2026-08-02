#pragma warning disable CA2000

namespace Kafka.Engine;

partial class EngineFuncs
{
  internal const string KafkaConsumer = "kafka-consumer";

  internal static Activity? CreateMessageActivity<TKey, TValue>(
    ActivitySource activitySource,
    string activityName,
    ConsumeResult<TKey, TValue> result,
    Guid? messageId,
    Guid? correlationId,
    string component,
    ActivityKind activityKind = ActivityKind.Consumer) =>
      ExtractMessageActivityContext(result.Message.Headers) is ActivityContext activityContext ?
        CreateActivity(activitySource, activityName, activityKind, activityContext)?
          .SetComponentActivityTags(component)
          .SetMessageActivityTags(messageId, correlationId, result.TopicPartitionOffset, component) :
        default;

  static Activity? SetMessageActivityTags(
    this Activity? activity,
    Guid? messageId,
    Guid? correlationId,
    TopicPartitionOffset topicPartition,
    string component,
    string system = "kafka-client") =>
      activity?
        .AddTag(ActivityTagNames.KafkaTopic, topicPartition.Topic)
        .AddTag(ActivityTagNames.KafkaPartition, topicPartition.Partition.Value)
        .AddTag(ActivityTagNames.KafkaOffset, topicPartition.Offset)
        .AddTag(ActivityTagNames.MessageId, messageId)
        .AddTag(ActivityTagNames.CorrelationId, correlationId);


}