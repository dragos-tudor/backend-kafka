using static Kafka.Operations.Outbox.OutboxCounterType;

namespace Kafka.Operations.Outbox;

public enum OutboxCounterType
{
  PublishedOutboxCounter,
  PublishOutboxErrorCounter,
  ScheduleOutboxRetryCounter,
  ScheduleOutboxExhaustedCounter,
  ScheduleOutboxErrorCounter,
  DispatchedDeadLetterCounter,
  DispatchDeadLetterErrorCounter,
  DelayDeadLetterRetryCounter,
  DelayDeadLetterExhaustedCounter,
  DelayDeadLetterErrorCounter
}

partial class OutboxFuncs
{
  internal static OutboxCounters CreateOutboxCounters(Meter meter) =>
    ImmutableDictionary<OutboxCounterType, Counter<long>>.Empty
      .AddRange(new Dictionary<OutboxCounterType, Counter<long>>() {
        [PublishedOutboxCounter] = meter.CreateCounter<long>("published.outbox.messages"),
        [PublishOutboxErrorCounter] = meter.CreateCounter<long>("published.outbox.messages.error"),
        [ScheduleOutboxRetryCounter] = meter.CreateCounter<long>("schedule.outbox.retry"),
        [ScheduleOutboxExhaustedCounter] = meter.CreateCounter<long>("schedule.outbox.exhausted"),
        [ScheduleOutboxErrorCounter] = meter.CreateCounter<long>("schedule.outbox.error"),
        [DispatchedDeadLetterCounter] = meter.CreateCounter<long>("dispatched.deadletter"),
        [DispatchDeadLetterErrorCounter] = meter.CreateCounter<long>("dispatch.deadletter.error"),
        [DelayDeadLetterRetryCounter] = meter.CreateCounter<long>("delay.deadletter.retry"),
        [DelayDeadLetterExhaustedCounter] = meter.CreateCounter<long>("delay.deadletter.exhausted"),
        [DelayDeadLetterErrorCounter] = meter.CreateCounter<long>("delay.deadletter.error")
      });
}