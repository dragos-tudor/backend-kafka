
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
  internal static ImmutableDictionary<CapturingCounterType, Counter<long>> CapturingCounters =
    ImmutableDictionary<CapturingCounterType, Counter<long>>.Empty
      .AddRange(new Dictionary<CapturingCounterType, Counter<long>>() {
        [CapturedCounter] = Meter!.CreateCounter<long>("captured.kafka.messages"),
        [CaptureErrorCounter] = Meter.CreateCounter<long>("capture.kafka.messages.error"),
        [NotCapturedCounter] = Meter.CreateCounter<long>("not.captured.kafka.messages"),
      });
}