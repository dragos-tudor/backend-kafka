
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
        [InboxCounterType.CapturedCounter] = meter.CreateCounter<long>("kafka.messages.captured"),
        [InboxCounterType.CaptureErrorCounter] = meter.CreateCounter<long>("kafka.messages.capture.error"),
        [InboxCounterType.NotCapturedCounter] = meter.CreateCounter<long>("kafka.messages.not.captured"),
        [InboxCounterType.InsertedCounter] = meter.CreateCounter<long>("inbox.messages.inserted"),
        [InboxCounterType.InsertErrorCounter] = meter.CreateCounter<long>("inbox.messages.insert.error"),
        [InboxCounterType.HandledCounter] = meter.CreateCounter<long>("inbox.messages.handled"),
        [InboxCounterType.HandleTechnicalErrorCounter] = meter.CreateCounter<long>("handle.technical.error"),
        [InboxCounterType.ScheduleInboxRetryCounter] = meter.CreateCounter<long>("schedule.inbox.retry"),
        [InboxCounterType.ScheduleInboxExhaustedCounter] = meter.CreateCounter<long>("schedule.inbox.exhausted"),
        [InboxCounterType.ScheduleInboxErrorCounter] = meter.CreateCounter<long>("schedule.inbox.error"),
        [InboxCounterType.DispatchedDeadLetterCounter] = meter.CreateCounter<long>("dispatched.deadletter"),
        [InboxCounterType.DispatchDeadLetterErrorCounter] = meter.CreateCounter<long>("dispatch.deadletter.error"),
        [InboxCounterType.DelayDeadLetterRetryCounter] = meter.CreateCounter<long>("delay.deadletter.retry"),
        [InboxCounterType.DelayDeadLetterExhaustedCounter] = meter.CreateCounter<long>("delay.deadletter.exhausted"),
        [InboxCounterType.DelayDeadLetterErrorCounter] = meter.CreateCounter<long>("delay.deadletter.error")
      });
}