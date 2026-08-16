
namespace Kafka.Operations.Outbox;

static class ValidatingCounters
{
  internal static readonly Counter<long> ValidatedCounter = OutboxMeter.CreateCounter<long>("validated.outbox.messages");
  internal static readonly Counter<long> ValidateErrorCounter = OutboxMeter.CreateCounter<long>("validate.outbox.messages.error");
  internal static readonly Counter<long> ValidateDataErrorCounter = OutboxMeter.CreateCounter<long>("validate.outbox.messages.data.error");
  internal static readonly Counter<long> ValidatePayloadErrorCounter = OutboxMeter.CreateCounter<long>("validate.outbox.messages.payload.error");
}