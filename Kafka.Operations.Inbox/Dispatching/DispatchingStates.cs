
namespace Kafka.Operations.Inbox;

static class DispatchingStates
{
  internal const string DispatchedDeadLetterState = "DispatchedDeadLetterState";
  internal const string DispatchDeadLetterErrorState = "DispatchDeadLetterErrorState";
  internal const string DispatchDeadLetterCriticalErrorState = "DispatchDeadLetterCriticalErrorState";
}