
namespace Kafka.Messages;

partial class MessagesFuncs
{
  static byte[]? EncodeKafkaValue(string? value) =>
    value is not null ? Encoding.UTF8.GetBytes(value) : null;
}