
namespace Kafka.StateMachines;

public enum ConsumingCounterType
{
  ConsumedCounter,
  ConsumeCriticalErrorsCounter
}

partial class StateMachinesFuncs
{
  static ImmutableDictionary<ConsumingCounterType, Counter<long>> ConsumingCounters =
    ImmutableDictionary<ConsumingCounterType, Counter<long>>.Empty
      .AddRange(new Dictionary<ConsumingCounterType, Counter<long>>() {
        [ConsumingCounterType.ConsumedCounter] = Meter!.CreateCounter<long>("consumed.kafka.messages"),
        [ConsumingCounterType.ConsumeCriticalErrorsCounter] = Meter.CreateCounter<long>("consume.kafka.messages.critical.errors"),
      });
}