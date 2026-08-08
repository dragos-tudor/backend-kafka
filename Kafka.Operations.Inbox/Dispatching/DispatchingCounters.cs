
using static Kafka.Operations.Inbox.DispatchingCounterType;

namespace Kafka.Operations.Inbox;

public enum DispatchingCounterType
{
  DispatchedDeadLetterCounter,
  DispatchDeadLetterErrorCounter,
}

partial class InboxFuncs
{
  internal static IImmutableDictionary<DispatchingCounterType, Counter<long>> CreateDispatchingCounters(Meter meter) =>
    ImmutableDictionary<DispatchingCounterType, Counter<long>>.Empty
      .AddRange(new Dictionary<DispatchingCounterType, Counter<long>>() {
        [DispatchedDeadLetterCounter] = meter.CreateCounter<long>("dispatched.deadletter"),
        [DispatchDeadLetterErrorCounter] = meter.CreateCounter<long>("dispatch.deadletter.error")
      });
}