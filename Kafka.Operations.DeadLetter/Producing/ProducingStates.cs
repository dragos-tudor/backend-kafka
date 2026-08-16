
namespace Kafka.Operations.DeadLetter;

static class ProducingStates
{
  internal const string ProducedDeadLetterCallbackState = "ProducedDeadLetterCallbackState";
  internal const string ProduceDeadLetterCallbackDeliveryErrorState = "ProduceDeadLetterCallbackDeliveryErrorState";
  internal const string ProduceDeadLetterCallbackErrorState = "ProduceDeadLetterCallbackErrorState";

  internal const string ProducingKafkaDeadLetterState = "ProducingKafkaDeadLetterState";
  internal const string ProduceKafkaDeadLetterErrorState = "ProduceKafkaDeadLetterErrorState";
  internal const string ProduceKafkaDeadLetterCriticalErrorState = "ProduceKafkaDeadLetterCriticalErrorState";
}