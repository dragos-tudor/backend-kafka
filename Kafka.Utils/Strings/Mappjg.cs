
namespace Kafka.Utils;

partial class UtilsFuncs
{
  internal static IEnumerable<string> ToEnumerable(string? value)
  {
    if (value is null) yield break;
    yield return value;
  }
}