
namespace Kafka.Messages;

partial class MessagesFuncs
{
  static TPayload? ToIntegrationMessagePayload<TKey, TValue, TPayload>(Message<TKey, TValue> message, Func<TValue, TPayload> mapper) =>
    message.Value is not null ? mapper(message.Value) : default;
}