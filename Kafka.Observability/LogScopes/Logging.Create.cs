
namespace Kafka.Observability;

partial class ObservabilityFuncs
{
  internal static IDisposable CreateLogScopeForActivity(
    ILogger logger,
    Activity? activity,
    string? component = null)
  {
    var scope = new Dictionary<string, object?>
    {
      ["traceId"] = activity?.TraceId.ToString(),
      ["spanId"] = activity?.SpanId.ToString()
    };
    if (component is not null) scope["component"] = component;
    return logger.BeginScope(scope)!;
  }
}