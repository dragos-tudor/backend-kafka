
namespace Kafka.Operations.Outbox;

static class DelayingStates
{
  internal const string DelayDeadLetterExhaustedState = "DelayDeadLetterExhaustedState";
  internal const string DelayDeadLetterRetryState = "DelayDeadLetterRetryState";
  internal const string DelayDeadLetterErrorState = "DelayDeadLetterErrorState";
}