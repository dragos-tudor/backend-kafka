
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
  internal static IImmutableDictionary<SchedulingCounterType, Counter<long>> CreateSchedulingCounters(Meter meter) =>
    ImmutableDictionary<SchedulingCounterType, Counter<long>>.Empty
      .AddRange(new Dictionary<SchedulingCounterType, Counter<long>>() {
        [ScheduleOutboxRetryCounter] = meter.CreateCounter<long>("schedule.outbox.retry"),
        [ScheduleOutboxExhaustedCounter] = meter.CreateCounter<long>("schedule.outbox.exhausted"),
        [ScheduleOutboxErrorCounter] = meter.CreateCounter<long>("schedule.outbox.error")
      });
}