
namespace Kafka.Messages;

partial class MessagesFuncs
{
  internal static Message<TKey, TValue> ToKafkaDeadLetter<TKey, TValue, TPayload>(
    IntegrationMessage<TKey, TPayload> message,
    TopicPartitionOffset topicPartitionOffset,
    string failureReason,
    DateTime date,
    Func<TPayload, TValue> mapper)
  =>
    CreateKafkaDeadLetter(
      message.MessageKey,
      mapper(message.Payload!),
      SetKafkaMessageHeaders(
        [],
        message.MessageId,
        message.Type,
        message.Version,
        message.CorrelationId),
      topicPartitionOffset,
      failureReason,
      date);
}