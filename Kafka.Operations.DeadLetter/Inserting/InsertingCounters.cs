
namespace Kafka.Operations.DeadLetter;

static class InsertingCounters
{
  internal static readonly Counter<long> InsertedCounter = DeadLetterMeter.CreateCounter<long>("inserted.dead.letter.messages");
  internal static readonly Counter<long> InsertErrorCounter = DeadLetterMeter.CreateCounter<long>("insert.dead.letter.messages.error");
}