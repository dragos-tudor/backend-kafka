
namespace Kafka.StateMachines;

public enum ResumingCounterType
{
  ResumedCounter,
  ResumingCriticalErrorsCounter
}

partial class ResumingFuncs
{
  internal static ImmutableDictionary<ResumingCounterType, Counter<long>> CreateResumingCounters(Meter meter) =>
    ImmutableDictionary<ResumingCounterType, Counter<long>>.Empty
      .AddRange(new Dictionary<ResumingCounterType, Counter<long>>() {
        [ResumingCounterType.ResumedCounter] = meter.CreateCounter<long>("kafka.inbox.resumed"),
        [ResumingCounterType.ResumingCriticalErrorsCounter] = meter.CreateCounter<long>("kafka.inbox.resuming.critical.errors"),
      });
}