
namespace Kafka.Pipelines;

static class RedeliveringStates
{
  internal const string RedeliveringNotStartedState = "Redelivering dead letter messages not started";
  internal const string RedeliveringCriticalErrorState = "Redelivering dead letter messages critical error.";
}
