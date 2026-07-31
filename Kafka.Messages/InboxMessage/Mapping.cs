
namespace Kafka.Messages;

partial class MessagesFuncs
{
  internal static InboxMessage<TKey, TPayload> ToInboxMessage<TKey, TValue, TPayload>(
    Message<TKey, TValue> message,
    TopicPartitionOffset topicPartitionOffset,
    Func<TValue, TPayload> mapper,
    DateTime receivedDate,
    InboxMessageStatus status = InboxMessageStatus.Pending,
    int retryCount = 0,
    DateTime? nextAttemptAt = default,
    string? error = default)
  =>
    new()
    {
      MessageId = GetMessageIdKafkaHeader(message.Headers) ?? GetNewPersistedMessageId(),
      MessageKey = message.Key,
      Payload = ToPersistedMessagePayload(message, mapper),
      Date = message.Timestamp.UtcDateTime,
      ReceivedAt = receivedDate,
      Type = GetSchemaTypeKafkaHeader(message.Headers),
      Version = GetSchemaVersionKafkaHeader(message.Headers),
      Metadata = SerializeTopicPartitionOffset(topicPartitionOffset),
      RetryCount = retryCount,
      LastError = error,
      Status = status,
      NextAttemptAt = nextAttemptAt,
      CorrelationId = GetCorrelationIdKafkaHeader(message.Headers)
    };
}
