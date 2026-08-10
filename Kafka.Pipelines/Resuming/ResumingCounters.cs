
namespace Kafka.Pipelines;

static class ResumingCounters
{
  internal static readonly Counter<long> ResumedCounter = PipelinesMeter!.CreateCounter<long>("resumed.inbox.messages");
  internal static readonly Counter<long> ResumeCriticalErrorsCounter = PipelinesMeter!.CreateCounter<long>("resume.inbox.messages.critical.errors");
  internal static readonly Counter<long> FetchErrorCounter = PipelinesMeter!.CreateCounter<long>("fetch.inbox.messages.error");
}