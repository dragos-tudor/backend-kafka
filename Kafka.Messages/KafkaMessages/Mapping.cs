
namespace Kafka.Messages;

partial class MessagesFuncs
{
  internal static Message<TKey, TValue> ToKafkaMessage<TKey, TValue, TPayload>(
    OutboxMessage<TKey, TPayload> message,
    TValue value,
    DateTime date)
  =>
    CreateKafkaMessage(
      message.MessageKey,
      value,
      SetKafkaMessageHeaders(
        [],
        message.MessageId,
        message.Type,
        message.Version,
        message.CorrelationId),
      date);
}