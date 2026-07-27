
using System.Text.Json;

namespace Kafka.Messages;

partial class MessagesFuncs
{
  internal static byte[] SerializeJson<T>(T value, JsonSerializerOptions? serializerOptions = default)
    => JsonSerializer.SerializeToUtf8Bytes(value, serializerOptions);
}
