
namespace Kafka.StateMachines;

static class ResumingCounters
{
  internal static readonly Counter<long> ResumedCounter = StateMachinesMeter!.CreateCounter<long>("resumed.inbox.messages");
  internal static readonly Counter<long> ResumeCriticalErrorsCounter = StateMachinesMeter!.CreateCounter<long>("resume.inbox.messages.critical.errors");
  internal static readonly Counter<long> FetchErrorCounter = StateMachinesMeter!.CreateCounter<long>("fetch.inbox.messages.error");
}