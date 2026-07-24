
namespace Kafka.Client;

partial class KafkaFuncs
{
  public static Message<TKey, byte[]> CreateKafkaMessageFromOutbox<TKey>(OutboxMessage outboxMessage, TKey key)
  =>
    CreateKafkaMessage(
      key,
      outboxMessage.Payload,
      SetKafkaMessageHeaders(
        new Headers(),
        outboxMessage.Type,
        outboxMessage.Version,
        outboxMessage.TraceParent,
        outboxMessage.MessageId,
        outboxMessage.CorrelationId),
      outboxMessage.Date);
}
