
namespace Kafka.Messages;

partial class MessagesFuncs
{
  static Message<TKey, TValue> ToKafkaMessage<TKey, TValue, TPayload>(
    PersistedMessage<TKey, TPayload> message,
    Func<TPayload, TValue> mapper) =>
      CreateKafkaMessage(
        message.MessageKey,
        ToKafkaMessageValue(message, mapper)!,
        SetKafkaMessageHeaders(
          [],
          message.MessageId,
          message.Type,
          message.Version,
          message.CorrelationId
        ),
        message.Date
      );

  static TValue? ToKafkaMessageValue<TKey, TValue, TPayload>(PersistedMessage<TKey, TPayload> message, Func<TPayload, TValue> mapper) =>
    message.Payload is not null ? mapper(message.Payload) : default;
}
