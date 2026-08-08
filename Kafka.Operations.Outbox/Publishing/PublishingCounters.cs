using static Kafka.Operations.Outbox.PublishingCounterType;

namespace Kafka.Operations.Outbox;

public enum PublishingCounterType
{
  PublishedOutboxCounter,
  PublishOutboxErrorCounter
}

partial class OutboxFuncs
{
  internal static IImmutableDictionary<PublishingCounterType, Counter<long>> CreatePublishingCounters(Meter meter) =>
    ImmutableDictionary<PublishingCounterType, Counter<long>>.Empty
      .AddRange(new Dictionary<PublishingCounterType, Counter<long>>() {
        [PublishedOutboxCounter] = meter.CreateCounter<long>("published.outbox.messages"),
        [PublishOutboxErrorCounter] = meter.CreateCounter<long>("published.outbox.messages.error")
      });
}