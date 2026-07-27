
namespace Kafka.Messages;

partial class MessagesFuncs
{
  internal static InboxMessage<TKey, TPayload> ToInboxMessage<TKey, TValue, TPayload>(
    Message<TKey, TValue> message,
    TopicPartitionOffset topicPartitionOffset,
    Func<TValue, TPayload> mapper,
    InboxMessageStatus status = InboxMessageStatus.Pending,
    int retryCount = 0,
    DateTime? nextAttemptAt = default,
    string? failureReason = default)
  =>
    new()
    {
      MessageId = GetMessageIdKafkaHeader(message.Headers) ?? GetNewMessageId(),
      MessageKey = message.Key,
      Payload = ToMessagePayload(message, mapper),
      Date = message.Timestamp.UtcDateTime,
      Type = GetSchemaTypeKafkaHeader(message.Headers),
      Version = GetSchemaVersionKafkaHeader(message.Headers),
      Metadata = SerializeTopicPartitionOffset(topicPartitionOffset),
      RetryCount = retryCount,
      LastFailureReason = failureReason,
      Status = status,
      NextAttemptAt = nextAttemptAt,
      CorrelationId = GetCorrelationIdKafkaHeader(message.Headers)
    };
}
