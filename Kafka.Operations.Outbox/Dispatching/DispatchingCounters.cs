
using static Kafka.Operations.Outbox.DispatchingCounterType;

namespace Kafka.Operations.Outbox;

public enum DispatchingCounterType
{
  DispatchedDeadLetterCounter,
  DispatchDeadLetterErrorCounter,
}

partial class OutboxFuncs
{
  internal static IImmutableDictionary<DispatchingCounterType, Counter<long>> DispatchingCounters =
    ImmutableDictionary<DispatchingCounterType, Counter<long>>.Empty
      .AddRange(new Dictionary<DispatchingCounterType, Counter<long>>() {
        [DispatchedDeadLetterCounter] = Meter.CreateCounter<long>("dispatched.deadletter"),
        [DispatchDeadLetterErrorCounter] = Meter.CreateCounter<long>("dispatch.deadletter.error")
      });
}