
using static Kafka.Operations.Inbox.InsertingCounterType;

namespace Kafka.Operations.Inbox;

public enum InsertingCounterType
{
  InsertedCounter,
  InsertErrorCounter,
}

partial class InboxFuncs
{
  internal static IImmutableDictionary<InsertingCounterType, Counter<long>> InsertingCounters =
    ImmutableDictionary<InsertingCounterType, Counter<long>>.Empty
      .AddRange(new Dictionary<InsertingCounterType, Counter<long>>() {
        [InsertedCounter] = Meter.CreateCounter<long>("inserted.inbox.messages"),
        [InsertErrorCounter] = Meter.CreateCounter<long>("insert.inbox.messages.error")
      });
}