
namespace Kafka.Operations.Outbox;

static class SchedulingStates
{
  internal const string ScheduleOutboxMessageExhaustedState = "ScheduleOutboxMessageExhaustedState";
  internal const string ScheduleOutboxMessageRetryState = "ScheduleOutboxMessageRetryState";
  internal const string ScheduleOutboxMessageErrorState = "ScheduleOutboxMessageErrorState";
}