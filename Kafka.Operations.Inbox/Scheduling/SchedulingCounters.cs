
namespace Kafka.Operations.Inbox;

static class SchedulingCounters
{
  internal static readonly Counter<long> ScheduleInboxRetryCounter = InboxMeter.CreateCounter<long>("schedule.inbox.retry");
  internal static readonly Counter<long> ScheduleInboxExhaustedCounter = InboxMeter.CreateCounter<long>("schedule.inbox.exhausted");
  internal static readonly Counter<long> ScheduleInboxErrorCounter = InboxMeter.CreateCounter<long>("schedule.inbox.error");
}