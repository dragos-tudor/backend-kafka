
namespace Kafka.Operations.Outbox;

static class MappingCounters
{
  internal static readonly Counter<long> MappedCounter = OutboxMeter.CreateCounter<long>("mapped.kafka.messages");
  internal static readonly Counter<long> MapErrorCounter = OutboxMeter.CreateCounter<long>("map.kafka.messages.error");
  internal static readonly Counter<long> MapPayloadErrorCounter = OutboxMeter.CreateCounter<long>("map.kafka.messages.payload.error");
}