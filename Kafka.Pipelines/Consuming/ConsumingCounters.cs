
namespace Kafka.Pipelines;

static class ConsumingCounters
{
  internal static readonly Counter<long> ConsumedCounter = PipelinesMeter!.CreateCounter<long>("consumed.kafka.messages");
  internal static readonly Counter<long> ConsumeCriticalErrorsCounter = PipelinesMeter!.CreateCounter<long>("consume.kafka.messages.critical.errors");
}