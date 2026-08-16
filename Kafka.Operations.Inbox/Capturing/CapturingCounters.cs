
namespace Kafka.Operations.Inbox;

static class CapturingCounters
{
  internal static readonly Counter<long> CapturedCounter = InboxMeter.CreateCounter<long>("captured.kafka.messages");
  internal static readonly Counter<long> CaptureErrorCounter = InboxMeter.CreateCounter<long>("capture.kafka.messages.error");
  internal static readonly Counter<long> CaptureCriticalErrorCounter = InboxMeter.CreateCounter<long>("capture.kafka.messages.critical.error");
  internal static readonly Counter<long> NotCapturedCounter = InboxMeter.CreateCounter<long>("not.captured.kafka.messages");
}