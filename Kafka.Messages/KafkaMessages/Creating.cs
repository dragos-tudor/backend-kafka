
namespace Kafka.Messages;

partial class MessagesFuncs
{
  internal static Message<TKey, TPayload> CreateKafkaMessage<TKey, TPayload>(
    TKey key,
    TPayload value,
    Headers headers,
    DateTime date = default)
  =>
    new()
    {
      Key = key,
      Value = value,
      Headers = headers,
      Timestamp = GetKafkaMessageTimestamp(date)
    };
}