
namespace Kafka.Messages;

partial class MessagesFuncs
{
  static TPayload? ToInboxMessagePayload<TKey, TValue, TPayload>(Message<TKey, TValue> message, Func<TValue, TPayload> mapper) =>
    message.Value is not null ? mapper(message.Value) : default;

  internal static InboxMessage<TKey, TPayload> ToInboxMessage<TKey, TValue, TPayload>(
    Message<TKey, TValue> message,
    TopicPartitionOffset topicPartitionOffset,
    TPayload? payload,
    DateTime receivedDate,
    InboxMessageStatus status,
    string? lastError = default
  ) =>
    new()
    {
      MessageId = GetMessageIdKafkaHeader(message.Headers),
      MessageKey = message.Key,
      Payload = payload,
      Date = message.Timestamp.UtcDateTime,
      ReceivedAt = receivedDate,
      Status = status,
      Type = GetSchemaTypeKafkaHeader(message.Headers),
      Version = GetSchemaVersionKafkaHeader(message.Headers),
      Metadata = SerializeTopicPartitionOffset(topicPartitionOffset),
      CorrelationId = GetCorrelationIdKafkaHeader(message.Headers),
      LastError = lastError
    };
}
