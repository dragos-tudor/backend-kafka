
namespace Kafka.Messages;

partial class MessagesFuncs
{
  internal static Message<TKey, TValue> CreateKafkaDeadLetter<TKey, TValue>(
    Message<TKey, TValue> message,
    TopicPartitionOffset? topicPartitionOffset,
    string failureReason)
  =>
    CreateKafkaMessage(
      message.Key,
      message.Value,
      SetDeadLetterHeaders(message, topicPartitionOffset, failureReason),
      message.Timestamp.UtcDateTime
    );
}