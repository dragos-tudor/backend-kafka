using static Kafka.Operations.Outbox.PublishingCounterType;

namespace Kafka.Operations.Outbox;

public enum PublishingCounterType
{
  PublishedOutboxCounter,
  PublishOutboxErrorCounter
}

partial class OutboxFuncs
{
  internal static IImmutableDictionary<PublishingCounterType, Counter<long>> PublishingCounters =
    ImmutableDictionary<PublishingCounterType, Counter<long>>.Empty
      .AddRange(new Dictionary<PublishingCounterType, Counter<long>>() {
        [PublishedOutboxCounter] = Meter.CreateCounter<long>("published.outbox.messages"),
        [PublishOutboxErrorCounter] = Meter.CreateCounter<long>("published.outbox.messages.error")
      });
}