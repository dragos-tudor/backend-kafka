
using static Kafka.Operations.Inbox.CapturingCounterType;

namespace Kafka.Operations.Inbox;

public enum CapturingCounterType
{
  CapturedCounter,
  CaptureErrorCounter,
  NotCapturedCounter,
}

partial class InboxFuncs
{
  internal static IImmutableDictionary<CapturingCounterType, Counter<long>> CreateCapturingCounters(Meter meter) =>
    ImmutableDictionary<CapturingCounterType, Counter<long>>.Empty
      .AddRange(new Dictionary<CapturingCounterType, Counter<long>>() {
        [CapturedCounter] = meter.CreateCounter<long>("captured.kafka.messages"),
        [CaptureErrorCounter] = meter.CreateCounter<long>("capture.kafka.messages.error"),
        [NotCapturedCounter] = meter.CreateCounter<long>("not.captured.kafka.messages"),
      });
}