
namespace Kafka.Operations.Outbox;

static class ProducingCounters
{
  internal static readonly Counter<long> ProducingKafkaCounter = OutboxMeter.CreateCounter<long>("produced.kafka.messages");
  internal static readonly Counter<long> ProduceKafkaErrorCounter = OutboxMeter.CreateCounter<long>("produce.kafka.messages.error");
  internal static readonly Counter<long> ProduceKafkaDeliveryErrorCounter = OutboxMeter.CreateCounter<long>("produce.kafka.messages.delivery.error");
  internal static readonly Counter<long> ProduceKafkaCriticalErrorCounter = OutboxMeter.CreateCounter<long>("produce.kafka.messages.critical.error");
}