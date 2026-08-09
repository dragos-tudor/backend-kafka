
namespace Kafka.Operations.Outbox;

static class DispatchingStates
{
  internal const string DispatchedDeadLetterState = "DispatchedDeadLetterState";
  internal const string DispatchDeadLetterErrorState = "DispatchDeadLetterErrorState";
  internal const string DispatchDeadLetterCriticalErrorState = "DispatchDeadLetterCriticalErrorState";
}