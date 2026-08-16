
namespace Kafka.Operations.DeadLetter;

static class SchedulingCounters
{
  internal static readonly Counter<long> ScheduleDeadLetterRetryCounter = DeadLetterMeter.CreateCounter<long>("schedule.deadtetter.retry");
  internal static readonly Counter<long> ScheduleDeadLetterExhaustedCounter = DeadLetterMeter.CreateCounter<long>("schedule.deadtetter.exhausted");
  internal static readonly Counter<long> ScheduleDeadLetterErrorCounter = DeadLetterMeter.CreateCounter<long>("schedule.deadtetter.error");
}