
namespace Kafka.Operations.Inbox;

partial class InboxFuncs
{
  static TPayload RequireInboxMessagePayload<TPayload>(
    TPayload payload) =>
    payload ?? throw new InvalidOperationException("Inbox message payload is required.");
}