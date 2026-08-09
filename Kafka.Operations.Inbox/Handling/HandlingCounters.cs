
namespace Kafka.Operations.Inbox;

static class HandlingCounters
{
  internal static readonly Counter<long> HandledCounter = InboxMeter.CreateCounter<long>("handled.inbox.messages");
  internal static readonly Counter<long> HandleTechnicalErrorCounter = InboxMeter.CreateCounter<long>("handle.inbox.messages.technical.error");
}