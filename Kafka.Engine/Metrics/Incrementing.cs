
namespace Kafka.Engine;

partial class EngineFuncs
{
  internal static void IncrementMetricCounter(
    MetricCounters metricCounters,
    MetricCounterTypes counterType,
    long increment = 1)
  {
    metricCounters[counterType].Add(increment);
  }
}