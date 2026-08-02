
namespace Kafka.Observability;

partial class ObservabilityFuncs
{
  // manually inject the traceparent header from the activity context
  // TODO inject into baggage header as well
  internal static Headers InjectMessageActivityContext(
    Activity activity,
    Headers headers) =>
      SetTraceParentKafkaHeader(headers, ToTraceParent(activity));

  // delegate to OpenTelemetry TraceContextPropagator to inject the traceparent header from the activity context
  [Obsolete("Too many allocations for traceparent injection (CreatePropagationContext)")]
  static void InjectMessageActivityContext(
    Activity activity,
    Headers headers,
    TextMapPropagator? propagator = default) =>
      (propagator ?? Propagators.DefaultTextMapPropagator)
        .Inject(
          default, // CreatePropagationContext(activity),
          headers,
          SetTraceParentKafkaHeader(ToTraceParent(activity)));

}