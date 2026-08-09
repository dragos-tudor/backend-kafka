
namespace Kafka.Operations.Outbox;

static class DelayingCounters
{
  internal static readonly Counter<long> DelayDeadLetterRetryCounter = OutboxMeter.CreateCounter<long>("delay.deadletter.retry");
  internal static readonly Counter<long> DelayDeadLetterExhaustedCounter = OutboxMeter.CreateCounter<long>("delay.deadletter.exhausted");
  internal static readonly Counter<long> DelayDeadLetterErrorCounter = OutboxMeter.CreateCounter<long>("delay.deadletter.error");
}