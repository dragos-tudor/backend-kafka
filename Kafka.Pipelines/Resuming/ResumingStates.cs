
namespace Kafka.Pipelines;

static class ResumingStates
{
  internal const string ResumingNotStartedState = "Resuming inbox messages not started";
  internal const string ResumingCriticalErrorState = "Resuming inbox messages critical error.";
}
