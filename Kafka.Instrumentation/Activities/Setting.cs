
namespace Kafka.Instrumentation;

partial class InstrumentationFuncs
{
  internal static Activity? SetComponentActivityTags(
    this Activity? activity,
    string component,
    string system) =>
      activity?
        .AddTag(ActivityTagNames.KafkaSystem, system)
        .AddTag(ActivityTagNames.Component, component);
}