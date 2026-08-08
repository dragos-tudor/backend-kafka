
using static Kafka.Operations.Inbox.SchedulingCounterType;

namespace Kafka.Operations.Inbox;

public enum SchedulingCounterType
{
  ScheduleInboxRetryCounter,
  ScheduleInboxExhaustedCounter,
  ScheduleInboxErrorCounter
}

partial class InboxFuncs
{
  internal static IImmutableDictionary<SchedulingCounterType, Counter<long>> CreateSchedulingCounters(Meter meter) =>
    ImmutableDictionary<SchedulingCounterType, Counter<long>>.Empty
      .AddRange(new Dictionary<SchedulingCounterType, Counter<long>>() {
        [ScheduleInboxRetryCounter] = meter.CreateCounter<long>("schedule.inbox.retry"),
        [ScheduleInboxExhaustedCounter] = meter.CreateCounter<long>("schedule.inbox.exhausted"),
        [ScheduleInboxErrorCounter] = meter.CreateCounter<long>("schedule.inbox.error")
      });
}