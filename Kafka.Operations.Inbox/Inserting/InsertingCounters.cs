
using static Kafka.Operations.Inbox.InsertingCounterType;

namespace Kafka.Operations.Inbox;

public enum InsertingCounterType
{
  InsertedCounter,
  InsertErrorCounter,
}

partial class InboxFuncs
{
  internal static IImmutableDictionary<InsertingCounterType, Counter<long>> CreateInsertingCounters(Meter meter) =>
    ImmutableDictionary<InsertingCounterType, Counter<long>>.Empty
      .AddRange(new Dictionary<InsertingCounterType, Counter<long>>() {
        [InsertedCounter] = meter.CreateCounter<long>("inserted.inbox.messages"),
        [InsertErrorCounter] = meter.CreateCounter<long>("insert.inbox.messages.error")
      });
}