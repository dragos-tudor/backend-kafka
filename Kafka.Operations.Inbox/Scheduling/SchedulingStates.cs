
namespace Kafka.Operations.Inbox;

static class SchedulingStates
{
  internal const string ScheduleInboxMessageExhaustedState = "ScheduleInboxMessageExhaustedState";
  internal const string ScheduleInboxMessageRetryState = "ScheduleInboxMessageRetryState";
  internal const string ScheduleInboxMessageErrorState = "ScheduleInboxMessageErrorState";
}