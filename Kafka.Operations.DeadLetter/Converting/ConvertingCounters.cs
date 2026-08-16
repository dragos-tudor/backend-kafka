
namespace Kafka.Operations.DeadLetter;

static class ConvertingCounters
{
  internal static readonly Counter<long> ConvertedCounter = DeadLetterMeter.CreateCounter<long>("converted.deadletter.messages");
  internal static readonly Counter<long> ConvertErrorCounter = DeadLetterMeter.CreateCounter<long>("convert.deadletter.messages.error");
}