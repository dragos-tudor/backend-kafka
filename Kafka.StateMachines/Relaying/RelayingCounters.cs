
namespace Kafka.StateMachines;

static class RelayingCounters
{
  internal static readonly Counter<long> RelayedCounter = StateMachinesMeter!.CreateCounter<long>("relayed.outbox.messages");
  internal static readonly Counter<long> RelayCriticalErrorsCounter = StateMachinesMeter!.CreateCounter<long>("relay.outbox.messages.critical.errors");
  internal static readonly Counter<long> FetchErrorCounter = StateMachinesMeter!.CreateCounter<long>("fetch.outbox.messages.error");
}