
namespace Kafka.Instrumentation;

partial class InstrumentationFuncs
{
  internal static void AddMetricCounter<TCounterType>(
    IImmutableDictionary<TCounterType, Counter<long>> counters,
    TCounterType counterType,
    long delta = 1) where TCounterType : notnull =>
      counters[counterType].Add(delta);
}