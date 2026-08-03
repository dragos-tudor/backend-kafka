#pragma warning disable CA2000

namespace Kafka.Instrumentation;

partial class InstrumentationFuncs
{
  static ActivitySource EnsureActivitySourceListener(ActivitySource activitySource)
  {
    if (activitySource.HasListeners()) return activitySource;

    ActivitySource.AddActivityListener(new ActivityListener
    {
      ShouldListenTo = source => true,
      Sample = (ref options) =>
        ActivitySamplingResult.AllDataAndRecorded
    });
    return activitySource;
  }
}
