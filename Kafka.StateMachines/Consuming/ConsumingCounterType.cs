
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
        [ConsumingCounterType.ConsumedCounter] = meter.CreateCounter<long>("Kafka.Operations.Inbox.consumed"),
        [ConsumingCounterType.ConsumeCriticalErrorsCounter] = meter.CreateCounter<long>("Kafka.Operations.Inbox.consume.critical.errors"),
      });
}