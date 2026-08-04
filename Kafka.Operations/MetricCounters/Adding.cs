
namespace Kafka.Operations;

partial class OperationsFuncs
{
  internal static void AddMetricCounter(
    MetricCounters metricCounters,
    MetricCounterType counterType,
    long delta = 1)
  {
    metricCounters[counterType].Add(delta);
  }
}