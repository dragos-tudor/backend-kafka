
namespace Kafka.Utils;

partial class UtilsFuncs
{
  internal static byte[]? EncodeString(string? value) =>
    value is not null ? Encoding.UTF8.GetBytes(value) : null;
}