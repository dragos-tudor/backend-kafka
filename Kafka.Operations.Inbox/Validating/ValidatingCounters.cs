
namespace Kafka.Operations.Inbox;

static class ValidatingCounters
{
  internal static readonly Counter<long> ValidatedCounter = InboxMeter.CreateCounter<long>("validated.inbox.messages");
  internal static readonly Counter<long> ValidateErrorCounter = InboxMeter.CreateCounter<long>("validate.inbox.messages.error");
  internal static readonly Counter<long> ValidateDataErrorCounter = InboxMeter.CreateCounter<long>("validate.inbox.messages.data.error");
  internal static readonly Counter<long> ValidatePayloadErrorCounter = InboxMeter.CreateCounter<long>("validate.inbox.messages.payload.error");
}