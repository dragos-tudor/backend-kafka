
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
  internal static IImmutableDictionary<DelayingCounterType, Counter<long>> DelayingCounters =
    ImmutableDictionary<DelayingCounterType, Counter<long>>.Empty
      .AddRange(new Dictionary<DelayingCounterType, Counter<long>>() {
        [DelayDeadLetterRetryCounter] = Meter!.CreateCounter<long>("delay.deadletter.retry"),
        [DelayDeadLetterExhaustedCounter] = Meter.CreateCounter<long>("delay.deadletter.exhausted"),
        [DelayDeadLetterErrorCounter] = Meter.CreateCounter<long>("delay.deadletter.error")
      });
}