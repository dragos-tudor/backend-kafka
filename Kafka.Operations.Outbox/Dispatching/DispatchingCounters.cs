
using static Kafka.Operations.Outbox.DispatchingCounterType;

namespace Kafka.Operations.Outbox;

public enum DispatchingCounterType
{
  DispatchedDeadLetterCounter,
  DispatchDeadLetterErrorCounter,
}

partial class OutboxFuncs
{
  internal static IImmutableDictionary<DispatchingCounterType, Counter<long>> CreateDispatchingCounters(Meter meter) =>
    ImmutableDictionary<DispatchingCounterType, Counter<long>>.Empty
      .AddRange(new Dictionary<DispatchingCounterType, Counter<long>>() {
        [DispatchedDeadLetterCounter] = meter.CreateCounter<long>("dispatched.deadletter"),
        [DispatchDeadLetterErrorCounter] = meter.CreateCounter<long>("dispatch.deadletter.error")
      });
}