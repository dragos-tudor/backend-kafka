
namespace Kafka.Operations.Inbox;

static class DelayingStates
{
  internal const string DelayDeadLetterRetryState = "DelayDeadLetterRetryState";
  internal const string DelayDeadLetterExhaustedState = "DelayDeadLetterExhaustedState";
  internal const string DelayDeadLetterErrorState = "DelayDeadLetterErrorState";
}