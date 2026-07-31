
namespace Kafka.Utils;

partial class UtilsFuncs
{
  internal static string? DecodeString(byte[]? value) =>
    value is not null ? Encoding.UTF8.GetString(value) : default;
}