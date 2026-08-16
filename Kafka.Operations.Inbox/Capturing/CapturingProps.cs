
namespace Kafka.Operations.Inbox;

public interface IKafkaMessageProp<TKey, TValue> { Message<TKey, TValue>? KafkaMessage { get; set; } }

public interface ITopicPartitionOffsetProp { TopicPartitionOffset? TopicPartitionOffset { get; set; } }

partial class InboxFuncs
{
  static Message<TKey, TValue> RequireKafkaMessage<TKey, TValue>(
    Message<TKey, TValue>? message) =>
    message ?? throw new InvalidOperationException("Kafka message is required.");

  static TopicPartitionOffset RequireTopicPartitionOffset(
    TopicPartitionOffset? topicPartitionOffset) =>
    topicPartitionOffset ?? throw new InvalidOperationException("Topic partition offset is required.");
}