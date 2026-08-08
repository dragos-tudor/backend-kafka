
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
  internal static IImmutableDictionary<SchedulingCounterType, Counter<long>> SchedulingCounters =>
    ImmutableDictionary<SchedulingCounterType, Counter<long>>.Empty
      .AddRange(new Dictionary<SchedulingCounterType, Counter<long>>() {
        [ScheduleInboxRetryCounter] = Meter.CreateCounter<long>("schedule.inbox.retry"),
        [ScheduleInboxExhaustedCounter] = Meter.CreateCounter<long>("schedule.inbox.exhausted"),
        [ScheduleInboxErrorCounter] = Meter.CreateCounter<long>("schedule.inbox.error")
      });
}