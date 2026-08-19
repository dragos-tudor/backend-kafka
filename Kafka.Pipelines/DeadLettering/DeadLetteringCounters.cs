
namespace Kafka.Pipelines;

static class DeadLetteringCounters
{
  internal static readonly Counter<long> DeadLetteredCounter = PipelinesMeter!.CreateCounter<long>("deadlettered.inbox.messages");
  internal static readonly Counter<long> FetchErrorCounter = PipelinesMeter!.CreateCounter<long>("fetch.deadlettering.inbox.messages.error");
}
