
namespace Kafka.Pipelines;

static class RedeliveringCounters
{
  internal static readonly Counter<long> RedeliveredCounter = PipelinesMeter!.CreateCounter<long>("redelivered.dead.letter.messages");
  internal static readonly Counter<long> RedeliverCriticalErrorsCounter = PipelinesMeter!.CreateCounter<long>("redeliver.dead.letter.messages.critical.errors");
  internal static readonly Counter<long> FetchErrorCounter = PipelinesMeter!.CreateCounter<long>("fetch.dead.letter.messages.error");
}