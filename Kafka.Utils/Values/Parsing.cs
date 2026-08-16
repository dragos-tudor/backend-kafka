
namespace Kafka.Utils;

partial class UtilsFuncs
{
  internal static TEnum ParseEnumValue<TEnum>(string? value, TEnum fallback)
    where TEnum : struct, Enum
    => Enum.TryParse<TEnum>(value, true, out var parsed) ? parsed : fallback;

  internal static int ParseIntValue(string? value, int fallback)
    => int.TryParse(value, out var parsed) ? parsed : fallback;

  internal static long ParseLongValue(string? value, long fallback)
    => long.TryParse(value, out var parsed) ? parsed : fallback;

  internal static double ParseDoubleValue(string? value, double fallback)
    => double.TryParse(value, out var parsed) ? parsed : fallback;

  internal static bool ParseBoolValue(string? value, bool fallback)
    => bool.TryParse(value, out var parsed) ? parsed : fallback;

  internal static int? TryParseIntValue(string? value)
    => int.TryParse(value, out var parsed) ? parsed : null;

  internal static long? TryParseLongValue(string? value)
    => long.TryParse(value, out var parsed) ? parsed : null;
}