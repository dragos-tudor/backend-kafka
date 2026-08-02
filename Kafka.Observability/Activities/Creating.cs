#pragma warning disable CA2000

namespace Kafka.Observability;

partial class ObservabilityFuncs
{
  internal static Activity? CreateActivity(
    ActivitySource activitySource,
    string activityName,
    ActivityKind activityKind,
    ActivityContext? activityContext = default) =>
      activityContext is not null ?
        activitySource.StartActivity(activityName, activityKind, activityContext.Value) :
        activitySource.StartActivity(activityName, activityKind);

  internal static Activity? CreateComponentActivity(
    ActivitySource activitySource,
    string activityName,
    ActivityKind activityKind,
    string component) =>
      CreateActivity(activitySource, activityName, activityKind)?
        .SetComponentActivityTags(component);
}
