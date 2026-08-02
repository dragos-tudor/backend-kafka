
namespace Kafka.Observability;

partial class ObservabilityFuncs
{
  internal static Activity? SetComponentActivityTags(
    this Activity? activity,
    string component,
    string system = "kafka-client") =>
      activity?
        .AddTag(ActivityTagNames.KafkaSystem, system)
        .AddTag(ActivityTagNames.Component, component);
}