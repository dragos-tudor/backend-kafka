
namespace Kafka.Client;

partial class KafkaFuncs
{
  public static Message<TKey, byte[]> CreateKafkaDeadLetter<TKey>(
    string reason,
    Message<TKey, byte[]> message,
    TopicPartitionOffset topicPartitionOffset)
  =>
    CreateKafkaMessage(
      message.Key,
      message.Value,
      SetDeadLetterHeaders(reason, message, topicPartitionOffset),
      message.Timestamp.UtcDateTime
    );

  public static Message<TKey, byte[]> CreateKafkaDeadLetter<TKey>(
    string reason,
    Message<TKey, byte[]> message)
  =>
    CreateKafkaMessage(
      message.Key,
      message.Value,
      SetDeadLetterHeaders(reason, message),
      message.Timestamp.UtcDateTime
    );
}