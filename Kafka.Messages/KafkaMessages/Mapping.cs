
namespace Kafka.Messages;

partial class MessagesFuncs
{
  static Message<TKey, TValue> ToKafkaMessage<TKey, TValue, TPayload>(
    MessageBase<TKey, TPayload> message,
    Func<TPayload, TValue> mapper) =>
      CreateKafkaMessage(
        message.MessageKey,
        ToMessageValue(message, mapper)!,
        SetKafkaMessageHeaders(
          [],
          message.MessageId,
          message.Type,
          message.Version,
          message.CorrelationId
        ),
        message.Date
      );
}
