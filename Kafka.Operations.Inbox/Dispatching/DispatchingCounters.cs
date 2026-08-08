
using static Kafka.Operations.Inbox.DispatchingCounterType;

namespace Kafka.Operations.Inbox;

public enum DispatchingCounterType
{
  DispatchedDeadLetterCounter,
  DispatchDeadLetterErrorCounter,
}

partial class InboxFuncs
{
  internal static IImmutableDictionary<DispatchingCounterType, Counter<long>> DispatchingCounters =
    ImmutableDictionary<DispatchingCounterType, Counter<long>>.Empty
      .AddRange(new Dictionary<DispatchingCounterType, Counter<long>>() {
        [DispatchedDeadLetterCounter] = Meter.CreateCounter<long>("dispatched.deadletter"),
        [DispatchDeadLetterErrorCounter] = Meter.CreateCounter<long>("dispatch.deadletter.error")
      });
}