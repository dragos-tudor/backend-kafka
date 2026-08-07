
namespace Kafka.Instrumentation;

partial class InstrumentationFuncs
{
  const string SystemActivityKey = "kafka.system";
  const string ComponentActivityKey = "kafka.component";

  internal static Activity SetComponentActivityTags(
    this Activity activity,
    string component,
    string system) =>
      activity
        .AddTag(SystemActivityKey, system)
        .AddTag(ComponentActivityKey, component);
}