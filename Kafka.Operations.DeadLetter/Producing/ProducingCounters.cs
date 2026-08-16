
namespace Kafka.Operations.DeadLetter;

static class ProducingCounters
{
  internal static readonly Counter<long> ProducingKafkaCounter = DeadLetterMeter.CreateCounter<long>("produced.kafka.messages");
  internal static readonly Counter<long> ProduceKafkaErrorCounter = DeadLetterMeter.CreateCounter<long>("produce.kafka.messages.error");
  internal static readonly Counter<long> ProduceKafkaDeliveryErrorCounter = DeadLetterMeter.CreateCounter<long>("produce.kafka.messages.delivery.error");
  internal static readonly Counter<long> ProduceKafkaCriticalErrorCounter = DeadLetterMeter.CreateCounter<long>("produce.kafka.messages.critical.error");
}