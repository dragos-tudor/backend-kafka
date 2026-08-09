
namespace Kafka.Operations.Inbox;

static class DelayingCounters
{
  internal static readonly Counter<long> DelayDeadLetterRetryCounter = InboxMeter.CreateCounter<long>("delay.deadletter.retry");
  internal static readonly Counter<long> DelayDeadLetterExhaustedCounter = InboxMeter.CreateCounter<long>("delay.deadletter.exhausted");
  internal static readonly Counter<long> DelayDeadLetterErrorCounter = InboxMeter.CreateCounter<long>("delay.deadletter.error");
}