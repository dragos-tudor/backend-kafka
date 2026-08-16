
namespace Kafka.Operations.Inbox;

static class RedirectingStates
{
  internal const string RedirectedKafkaMessageState = "RedirectedKafkaMessageState";
  internal const string RedirectKafkaMessageAmbiguousState = "RedirectKafkaMessageAmbiguousState";
  internal const string RedirectKafkaMessageDeliveryErrorState = "RedirectKafkaMessageDeliveryErrorState";
  internal const string RedirectKafkaMessageErrorState = "RedirectKafkaMessageErrorState";
  internal const string RedirectKafkaMessageCriticalErrorState = "RedirectKafkaMessageCriticalErrorState";
}