
namespace Kafka.StateMachines;

public enum ResumingCounterType
{
  ResumedCounter,
  ResumeCriticalErrorsCounter,
  FetchErrorCounter
}

partial class ResumingFuncs
{
  internal static ImmutableDictionary<ResumingCounterType, Counter<long>> CreateResumingCounters(Meter meter) =>
    ImmutableDictionary<ResumingCounterType, Counter<long>>.Empty
      .AddRange(new Dictionary<ResumingCounterType, Counter<long>>() {
        [ResumingCounterType.ResumedCounter] = meter.CreateCounter<long>("resumed.inbox.messages"),
        [ResumingCounterType.ResumeCriticalErrorsCounter] = meter.CreateCounter<long>("resume.inbox.messages.critical.errors"),
        [ResumingCounterType.FetchErrorCounter] = meter.CreateCounter<long>("fetch.inbox.messages.error")
      });
}