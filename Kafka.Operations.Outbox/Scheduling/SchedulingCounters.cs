
namespace Kafka.Operations.Outbox;

static class SchedulingCounters
{
  internal static readonly Counter<long> ScheduleOutboxRetryCounter = OutboxMeter.CreateCounter<long>("schedule.outbox.retry");
  internal static readonly Counter<long> ScheduleOutboxExhaustedCounter = OutboxMeter.CreateCounter<long>("schedule.outbox.exhausted");
  internal static readonly Counter<long> ScheduleOutboxErrorCounter = OutboxMeter.CreateCounter<long>("schedule.outbox.error");
}