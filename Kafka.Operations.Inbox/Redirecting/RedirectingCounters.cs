
namespace Kafka.Operations.Inbox;

static class RedirectingCounters
{
  internal static readonly Counter<long> RedirectedKafkaCounter = InboxMeter.CreateCounter<long>("redirected.kafka.messages");
  internal static readonly Counter<long> RedirectKafkaErrorCounter = InboxMeter.CreateCounter<long>("redirect.kafka.messages.error");
  internal static readonly Counter<long> RedirectKafkaDeliveryWarningCounter = InboxMeter.CreateCounter<long>("redirect.kafka.messages.delivery.warning");
  internal static readonly Counter<long> RedirectKafkaCriticalErrorCounter = InboxMeter.CreateCounter<long>("redirect.kafka.messages.critical.error");
}