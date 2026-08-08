
using static Kafka.Operations.Outbox.SchedulingCounterType;

namespace Kafka.Operations.Outbox;

public enum SchedulingCounterType
{
  ScheduleOutboxRetryCounter,
  ScheduleOutboxExhaustedCounter,
  ScheduleOutboxErrorCounter
}

partial class OutboxFuncs
{
  internal static IImmutableDictionary<SchedulingCounterType, Counter<long>> SchedulingCounters =
    ImmutableDictionary<SchedulingCounterType, Counter<long>>.Empty
      .AddRange(new Dictionary<SchedulingCounterType, Counter<long>>() {
        [ScheduleOutboxRetryCounter] = Meter.CreateCounter<long>("schedule.outbox.retry"),
        [ScheduleOutboxExhaustedCounter] = Meter.CreateCounter<long>("schedule.outbox.exhausted"),
        [ScheduleOutboxErrorCounter] = Meter.CreateCounter<long>("schedule.outbox.error")
      });
}