
namespace Kafka.Pipelines;

static class RelayingCounters
{
  internal static readonly Counter<long> RelayedCounter = PipelinesMeter!.CreateCounter<long>("relayed.outbox.messages");
  internal static readonly Counter<long> RelayCriticalErrorsCounter = PipelinesMeter!.CreateCounter<long>("relay.outbox.messages.critical.errors");
  internal static readonly Counter<long> FetchErrorCounter = PipelinesMeter!.CreateCounter<long>("fetch.outbox.messages.error");
}