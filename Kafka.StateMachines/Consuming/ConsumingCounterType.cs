
namespace Kafka.StateMachines;

public enum ConsumingCounterType
{
  ConsumedCounter,
  ConsumeCriticalErrorsCounter
}

partial class ConsumingFuncs
{
  public static ImmutableDictionary<ConsumingCounterType, Counter<long>> CreateConsumingCounters(Meter meter) =>
    ImmutableDictionary<ConsumingCounterType, Counter<long>>.Empty
      .AddRange(new Dictionary<ConsumingCounterType, Counter<long>>() {
        [ConsumingCounterType.ConsumedCounter] = meter.CreateCounter<long>("consumed.kafka.messages"),
        [ConsumingCounterType.ConsumeCriticalErrorsCounter] = meter.CreateCounter<long>("consume.kafka.messages.critical.errors"),
      });
}