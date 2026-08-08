
namespace Kafka.Messages;

partial class MessagesFuncs
{
  internal static Message<TKey, TValue> ToKafkaMessage<TKey, TValue, TPayload>(
    IntegrationMessage<TKey, TPayload> message,
    DateTime date,
    Func<TPayload, TValue> mapper)
  =>
    CreateKafkaMessage(
      message.MessageKey,
      mapper(message.Payload!),
      SetKafkaMessageHeaders(
        [],
        message.MessageId,
        message.Type,
        message.Version,
        message.CorrelationId),
      date);
}