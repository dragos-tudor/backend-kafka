
namespace Kafka.Pipelines;

static class PublishingCounters
{
  internal static readonly Counter<long> PublishedCounter = PipelinesMeter!.CreateCounter<long>("published.outbox.messages");
  internal static readonly Counter<long> PublishCriticalErrorsCounter = PipelinesMeter!.CreateCounter<long>("published.outbox.messages.critical.errors");
}