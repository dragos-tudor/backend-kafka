
namespace Kafka.Messages;

partial class MessagesFuncs
{
  static TPayload? ToMessagePayload<TKey, TValue, TPayload>(Message<TKey, TValue> message, Func<TValue, TPayload> mapper) =>
    message.Value is not null ? mapper(message.Value) : default;

  static TValue? ToMessageValue<TKey, TValue, TPayload>(MessageBase<TKey, TPayload> message, Func<TPayload, TValue> mapper) =>
    message.Payload is not null ? mapper(message.Payload) : default;
}