
namespace Kafka.Operations.Outbox;

static class DispatchingCounters
{
  internal static readonly Counter<long> DispatchedDeadLetterCounter = OutboxMeter.CreateCounter<long>("dispatched.deadletter");
  internal static readonly Counter<long> DispatchDeadLetterErrorCounter = OutboxMeter.CreateCounter<long>("dispatch.deadletter.error");
}