
namespace Kafka.StateMachines;

public enum ResumingCounterType
{
  ResumedCounter,
  ResumeCriticalErrorsCounter,
  FetchErrorCounter
}

partial class StateMachinesFuncs
{
  internal static ImmutableDictionary<ResumingCounterType, Counter<long>> ResumingCounters =
    ImmutableDictionary<ResumingCounterType, Counter<long>>.Empty
      .AddRange(new Dictionary<ResumingCounterType, Counter<long>>() {
        [ResumingCounterType.ResumedCounter] = Meter.CreateCounter<long>("resumed.inbox.messages"),
        [ResumingCounterType.ResumeCriticalErrorsCounter] = Meter.CreateCounter<long>("resume.inbox.messages.critical.errors"),
        [ResumingCounterType.FetchErrorCounter] = Meter.CreateCounter<long>("fetch.inbox.messages.error")
      });
}