
namespace Kafka.Engine;

partial class EngineFuncs
{
  public static IImmutableDictionary<MetricCounterTypes, Counter<long>> CreateMeterCounters(Meter meter) =>
    ImmutableDictionary<MetricCounterTypes, Counter<long>>.Empty
      .Add(MetricCounterTypes.Consumed, meter.CreateCounter<long>("kafka.messages.consumed"))
      .Add(MetricCounterTypes.Handled, meter.CreateCounter<long>("kafka.messages.handled"))
      .Add(MetricCounterTypes.DeadLettered, meter.CreateCounter<long>("kafka.messages.deadlettered"))
      .Add(MetricCounterTypes.ConsumingErrors, meter.CreateCounter<long>("kafka.messages.consuming_errors"));
}