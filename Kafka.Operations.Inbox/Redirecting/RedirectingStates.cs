
namespace Kafka.Operations.Inbox;

static class RedirectingStates
{
  internal const string RedirectedKafkaMessageState = "RedirectedKafkaMessageState";
  internal const string RedirectKafkaMessageCircuitOpenState = "RedirectKafkaMessageCircuitOpenState";
  internal const string RedirectKafkaMessageErrorState = "RedirectKafkaMessageErrorState";
  internal const string RedirectKafkaMessageCriticalErrorState = "RedirectKafkaMessageCriticalErrorState";
}