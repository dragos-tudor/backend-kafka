
namespace Kafka.StateMachines;

partial class ConsumingFuncs
{
  public static ImmutableDictionary<ConsumingCounterType, Counter<long>> CreateConsumingCounters(Meter meter) =>
    ImmutableDictionary<ConsumingCounterType, Counter<long>>.Empty
      .AddRange(new Dictionary<ConsumingCounterType, Counter<long>>() {
        [ConsumingCounterType.ConsumedCounter] = meter.CreateCounter<long>("kafka.inbox.consumed"),
        [ConsumingCounterType.ConsumeCriticalErrorsCounter] = meter.CreateCounter<long>("kafka.inbox.consume.critical.errors"),
      });
}