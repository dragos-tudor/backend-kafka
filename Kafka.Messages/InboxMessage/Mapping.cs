
namespace Kafka.Messages;

partial class MessagesFuncs
{
  internal static InboxMessage<TKey, TPayload> ToInboxMessage<TKey, TValue, TPayload>(
    Message<TKey, TValue> message,
    TopicPartitionOffset topicPartitionOffset,
    Func<TValue, TPayload> mapper,
    DateTime receivedDate,
    InboxMessageStatus status = InboxMessageStatus.Pending)
  =>
    new()
    {
      MessageId = GetMessageIdKafkaHeader(message.Headers) ?? GetNewIntegrationMessageId(),
      MessageKey = message.Key,
      Payload = ToIntegrationMessagePayload(message, mapper),
      Date = message.Timestamp.UtcDateTime,
      ReceivedAt = receivedDate,
      Status = status,
      Type = GetSchemaTypeKafkaHeader(message.Headers),
      Version = GetSchemaVersionKafkaHeader(message.Headers),
      Metadata = SerializeTopicPartitionOffset(topicPartitionOffset),
      CorrelationId = GetCorrelationIdKafkaHeader(message.Headers)
    };
}
