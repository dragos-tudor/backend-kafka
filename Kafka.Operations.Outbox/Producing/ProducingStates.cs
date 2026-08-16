
namespace Kafka.Operations.Outbox;

static class ProducingStates
{
  internal const string ProducedCallbackState = "ProducedCallbackState";
  internal const string ProduceCallbackDeliveryErrorState = "ProduceCallbackDeliveryErrorState";
  internal const string ProduceCallbackErrorState = "ProduceCallbackErrorState";

  internal const string ProducingKafkaMessageState = "ProducingKafkaMessageState";
  internal const string ProduceKafkaMessageErrorState = "ProduceKafkaMessageErrorState";
  internal const string ProduceKafkaMessageCriticalErrorState = "ProduceKafkaMessageCriticalErrorState";
}