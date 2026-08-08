
using static Kafka.Operations.Inbox.InboxCounterType;

namespace Kafka.Operations.Inbox;

public enum InboxCounterType
{
  CapturedCounter,
  CaptureErrorCounter,
  NotCapturedCounter,
  InsertedCounter,
  InsertErrorCounter,
  HandledCounter,
  HandleTechnicalErrorCounter,
  ScheduleInboxRetryCounter,
  ScheduleInboxExhaustedCounter,
  ScheduleInboxErrorCounter,
  DispatchedDeadLetterCounter,
  DispatchDeadLetterErrorCounter,
  DelayDeadLetterRetryCounter,
  DelayDeadLetterExhaustedCounter,
  DelayDeadLetterErrorCounter
}

partial class InboxFuncs
{
  internal static InboxCounters CreateInboxCounters(Meter meter) =>
    ImmutableDictionary<InboxCounterType, Counter<long>>.Empty
      .AddRange(new Dictionary<InboxCounterType, Counter<long>>() {
        [CapturedCounter] = meter.CreateCounter<long>("captured.kafka.messages"),
        [CaptureErrorCounter] = meter.CreateCounter<long>("capture.kafka.messages.error"),
        [NotCapturedCounter] = meter.CreateCounter<long>("not.captured.kafka.messages"),
        [InsertedCounter] = meter.CreateCounter<long>("inserted.inbox.messages"),
        [InsertErrorCounter] = meter.CreateCounter<long>("insert.inbox.messages.error"),
        [HandledCounter] = meter.CreateCounter<long>("handled.inbox.messages"),
        [HandleTechnicalErrorCounter] = meter.CreateCounter<long>("handle.inbox.messages.technical.error"),
        [ScheduleInboxRetryCounter] = meter.CreateCounter<long>("schedule.inbox.retry"),
        [ScheduleInboxExhaustedCounter] = meter.CreateCounter<long>("schedule.inbox.exhausted"),
        [ScheduleInboxErrorCounter] = meter.CreateCounter<long>("schedule.inbox.error"),
        [DispatchedDeadLetterCounter] = meter.CreateCounter<long>("dispatched.deadletter"),
        [DispatchDeadLetterErrorCounter] = meter.CreateCounter<long>("dispatch.deadletter.error"),
        [DelayDeadLetterRetryCounter] = meter.CreateCounter<long>("delay.deadletter.retry"),
        [DelayDeadLetterExhaustedCounter] = meter.CreateCounter<long>("delay.deadletter.exhausted"),
        [DelayDeadLetterErrorCounter] = meter.CreateCounter<long>("delay.deadletter.error")
      });
}