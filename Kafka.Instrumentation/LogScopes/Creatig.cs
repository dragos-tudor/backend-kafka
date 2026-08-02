
namespace Kafka.Instrumentation;

partial class InstrumentationFuncs
{
  internal static IDisposable CreateLogScopeForActivity(
    ILogger logger,
    Activity? activity,
    string component)
  {
    var scope = new Dictionary<string, object?>
    {
      ["traceId"] = activity?.TraceId.ToString(),
      ["spanId"] = activity?.SpanId.ToString(),
      ["component"] = component
    };
    return logger.BeginScope(scope)!;
  }
}