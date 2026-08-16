
namespace Kafka.Operations.DeadLetter;

static class SchedulingStates
{
  internal const string ScheduleDeadLetterMessageExhaustedState = "ScheduleDeadLetterMessageExhaustedState";
  internal const string ScheduleDeadLetterMessageRetryState = "ScheduleDeadLetterMessageRetryState";
  internal const string ScheduleDeadLetterMessageErrorState = "ScheduleDeadLetterMessageErrorState";
}