
namespace Kafka.Operations.Inbox;

static class MappingCounters
{
  internal static readonly Counter<long> MappedCounter = InboxMeter.CreateCounter<long>("mapped.kafka.messages");
  internal static readonly Counter<long> MapErrorCounter = InboxMeter.CreateCounter<long>("map.kafka.messages.error");
  internal static readonly Counter<long> MapValueErrorCounter = InboxMeter.CreateCounter<long>("map.kafka.messages.value.error");
}