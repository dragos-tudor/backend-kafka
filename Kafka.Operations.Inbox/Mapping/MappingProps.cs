
namespace Kafka.Operations.Inbox;

public interface IInboxMessageProp<TKey, TPayload> { InboxMessage<TKey, TPayload>? InboxMessage { get; set; } }

public interface IInboxMessageErrorProp { string? InboxMessageError { get; set; } }

partial class InboxFuncs
{
  static InboxMessage<TKey,TPayload> RequireInboxMessage<TKey,TPayload>(
    InboxMessage<TKey,TPayload>? message) =>
    message ?? throw new InvalidOperationException("Inbox message is required.");

  static string RequireInboxMessageError(
    string? inboxMessageError) =>
    inboxMessageError ?? throw new InvalidOperationException("Inbox message error is required.");
}