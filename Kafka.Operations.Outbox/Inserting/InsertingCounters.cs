
namespace Kafka.Operations.Outbox;

static class InsertingCounters
{
  internal static readonly Counter<long> InsertedCounter = OutboxMeter.CreateCounter<long>("inserted.outbox.messages");
  internal static readonly Counter<long> InsertErrorCounter = OutboxMeter.CreateCounter<long>("insert.outbox.messages.error");
}