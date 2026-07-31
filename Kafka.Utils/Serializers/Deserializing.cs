
using System.Text.Json;

namespace Kafka.Utils;

partial class UtilsFuncs
{
  internal static T? DeserializeJson<T>(byte[]? value, JsonSerializerOptions? serializerOptions = default)
    => value is null || value.Length == 0
      ? default
      : JsonSerializer.Deserialize<T>(value, serializerOptions);
}