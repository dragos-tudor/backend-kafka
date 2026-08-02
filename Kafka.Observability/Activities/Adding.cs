
namespace Kafka.Observability;

partial class ObservabilityFuncs
{
  internal static Activity? AddActivityTag(Activity? activity, string key, object? value) =>
    activity?.AddTag(key, value);

  internal static Activity? AddActivityEvent(Activity? activity, string name, IEnumerable<KeyValuePair<string, object?>>? attributes = null)
  {
    var tags = attributes is null ? null : new ActivityTagsCollection(attributes);
    return activity?.AddEvent(new ActivityEvent(name, default, tags));
  }
}