
namespace Kafka.StateMachines;

public enum RelayingCounterType
{
  RelayedCounter,
  RelayCriticalErrorsCounter,
  FetchErrorCounter
}

partial class StateMachinesFuncs
{
  internal static ImmutableDictionary<RelayingCounterType, Counter<long>> RelayingCounters =
    ImmutableDictionary<RelayingCounterType, Counter<long>>.Empty
      .AddRange(new Dictionary<RelayingCounterType, Counter<long>>() {
        [RelayingCounterType.RelayedCounter] = Meter.CreateCounter<long>("relayed.outbox.messages"),
        [RelayingCounterType.RelayCriticalErrorsCounter] = Meter.CreateCounter<long>("relay.outbox.messages.critical.errors"),
        [RelayingCounterType.FetchErrorCounter] = Meter.CreateCounter<long>("fetch.outbox.messages.error")
      });
}