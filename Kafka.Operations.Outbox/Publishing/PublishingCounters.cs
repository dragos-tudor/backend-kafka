
namespace Kafka.Operations.Outbox;

static class PublishingCounters
{
  internal static readonly Counter<long> PublishedOutboxCounter = OutboxMeter.CreateCounter<long>("published.outbox.messages");
  internal static readonly Counter<long> PublishOutboxErrorCounter = OutboxMeter.CreateCounter<long>("published.outbox.messages.error");
}