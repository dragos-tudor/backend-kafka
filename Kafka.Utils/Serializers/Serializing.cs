
using System.Text.Json;

namespace Kafka.Utils;

partial class UtilsFuncs
{
  internal static byte[] SerializeJson<T>(T value, JsonSerializerOptions? serializerOptions = default)
    => JsonSerializer.SerializeToUtf8Bytes(value, serializerOptions);
}
