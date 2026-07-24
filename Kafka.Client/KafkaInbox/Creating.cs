
namespace Kafka.Client;

partial class KafkaFuncs
{
  public static InboxMessage CreateInboxMessage<TKey>(
    Message<TKey, byte[]> message,
    TopicPartitionOffset topicPartitionOffset)
  =>
    new()
    {
      MessageId = GetKafkaMessageIdHeader(message.Headers) ?? Guid.NewGuid(),
      Type = GetKafkaSchemaTypeHeader(message.Headers) ?? string.Empty,
      Payload = message.Value,
      Date = message.Timestamp.UtcDateTime,
      Version = GetKafkaSchemaVersionHeader(message.Headers),
      CorrelationId = GetKafkaCorrelationIdHeader(message.Headers) ?? Guid.Empty,
      TraceParent = GetKafkaTraceIdHeader(message.Headers),
      Topic = topicPartitionOffset.Topic,
      Partition = topicPartitionOffset.Partition.Value,
      Offset = topicPartitionOffset.Offset.Value
    };
}
