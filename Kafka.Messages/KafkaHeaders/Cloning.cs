
namespace Kafka.Messages;

partial class MessagesFuncs
{
  static Headers CloneKafkaHeaders(Headers? headers) =>
    (headers ?? []).Aggregate(new Headers(),
      (result, header) => SetKafkaHeaderValue(result, header.Key, header.GetValueBytes()));
}