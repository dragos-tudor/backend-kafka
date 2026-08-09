
namespace Kafka.Operations.Inbox;

static class DispatchingCounters
{
  internal static readonly Counter<long> DispatchedDeadLetterCounter = InboxMeter.CreateCounter<long>("dispatched.deadletter");
  internal static readonly Counter<long> DispatchDeadLetterErrorCounter = InboxMeter.CreateCounter<long>("dispatch.deadletter.error");
}