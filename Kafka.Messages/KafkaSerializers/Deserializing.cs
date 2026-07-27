
using System.Text.Json;

namespace Kafka.Messages;

partial class MessagesFuncs
{
  internal static T? DeserializeJson<T>(byte[]? value, JsonSerializerOptions? serializerOptions = default)
    => value is null || value.Length == 0
      ? default
      : JsonSerializer.Deserialize<T>(value, serializerOptions);
}