
namespace Kafka.Operations;

partial class OperationsFuncs
{
  public static IImmutableDictionary<MetricCounterType, Counter<long>> CreateMetricCounters(Meter meter) =>
    ImmutableDictionary<MetricCounterType, Counter<long>>.Empty
      .AddRange(new Dictionary<MetricCounterType, Counter<long>>() {
        [MetricCounterType.CapturedCounter] = meter.CreateCounter<long>("kafka.messages.captured"),
        [MetricCounterType.ConsumedCounter] = meter.CreateCounter<long>("kafka.messages.consumed"),
        [MetricCounterType.DeadLetteredCounter] = meter.CreateCounter<long>("kafka.messages.deadlettered"),
        [MetricCounterType.HandledCounter] = meter.CreateCounter<long>("kafka.messages.handled"),
        [MetricCounterType.InsertedCounter] = meter.CreateCounter<long>("kafka.messages.inserted"),
        [MetricCounterType.IdempotentCounter] = meter.CreateCounter<long>("kafka.messages.idempotent"),
        [MetricCounterType.ConsumingErrorsCounter] = meter.CreateCounter<long>("kafka.messages.consuming_errors")
      });
}