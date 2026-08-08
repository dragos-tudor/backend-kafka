
namespace Kafka.StateMachines;

public enum RelayingCounterType
{
  RelayedCounter,
  RelayCriticalErrorsCounter,
  FetchErrorCounter
}

partial class RelayingFuncs
{
  internal static ImmutableDictionary<RelayingCounterType, Counter<long>> CreateRelayingCounters(Meter meter) =>
    ImmutableDictionary<RelayingCounterType, Counter<long>>.Empty
      .AddRange(new Dictionary<RelayingCounterType, Counter<long>>() {
        [RelayingCounterType.RelayedCounter] = meter.CreateCounter<long>("relayed.outbox.messages"),
        [RelayingCounterType.RelayCriticalErrorsCounter] = meter.CreateCounter<long>("relay.outbox.messages.critical.errors"),
        [RelayingCounterType.FetchErrorCounter] = meter.CreateCounter<long>("fetch.outbox.messages.error")
      });
}