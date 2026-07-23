namespace Kafka.Client;

partial class KafkaFuncs
{
  static byte[]? EncodeKafkaValue(string? value) =>
    value is null ? null : Encoding.UTF8.GetBytes(value);
}