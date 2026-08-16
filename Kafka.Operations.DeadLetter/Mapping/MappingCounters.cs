
namespace Kafka.Operations.DeadLetter;

static class MappingCounters
{
  internal static readonly Counter<long> MappedCounter = DeadLetterMeter.CreateCounter<long>("mapped.kafka.messages");
  internal static readonly Counter<long> MapErrorCounter = DeadLetterMeter.CreateCounter<long>("map.kafka.messages.error");
  internal static readonly Counter<long> MapPayloadErrorCounter = DeadLetterMeter.CreateCounter<long>("map.kafka.messages.payload.error");
}