
using static Kafka.Operations.Outbox.DelayingCounterType;

namespace Kafka.Operations.Outbox;

public enum DelayingCounterType
{
  DelayDeadLetterRetryCounter,
  DelayDeadLetterExhaustedCounter,
  DelayDeadLetterErrorCounter
}

partial class OutboxFuncs
{
  internal static IImmutableDictionary<DelayingCounterType, Counter<long>> CreateDelayingCounters(Meter meter) =>
    ImmutableDictionary<DelayingCounterType, Counter<long>>.Empty
      .AddRange(new Dictionary<DelayingCounterType, Counter<long>>() {
        [DelayDeadLetterRetryCounter] = meter.CreateCounter<long>("delay.deadletter.retry"),
        [DelayDeadLetterExhaustedCounter] = meter.CreateCounter<long>("delay.deadletter.exhausted"),
        [DelayDeadLetterErrorCounter] = meter.CreateCounter<long>("delay.deadletter.error")
      });
}