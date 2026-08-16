
namespace Kafka.Pipelines;

static class RelayingStates
{
  internal const string RelayingNotStartedState = "Relaying outbox messages not started";
  internal const string RelayingCriticalErrorState = "Relaying outbox messages critical error.";
}
