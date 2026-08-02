
namespace Kafka.Instrumentation;

partial class InstrumentationFuncs
{
  internal static ActivityContext? ExtractActivityContext(string traceParent)
  {
    var parts = SplitTraceParent(traceParent);
    if (!HasMinTraceParentParts(parts)) return null;

    try
    {
      return new ActivityContext(
        ActivityTraceId.CreateFromString(parts[1].AsSpan()),
        ActivitySpanId.CreateFromString(parts[2].AsSpan()),
        GetActivityTraceFlags(parts[3])
      );
    }
    catch { return null; }
  }
}