
namespace Kafka.Operations.Inbox;

static class InsertingCounters
{
  internal static readonly Counter<long> InsertedCounter = InboxMeter.CreateCounter<long>("inserted.inbox.messages");
  internal static readonly Counter<long> InsertErrorCounter = InboxMeter.CreateCounter<long>("insert.inbox.messages.error");
}