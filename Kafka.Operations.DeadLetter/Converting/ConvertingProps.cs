
namespace Kafka.Operations.DeadLetter;

public interface IDeadLetterMessageProp<TKey, TPayload> { DeadLetterMessage<TKey, TPayload>? DeadLetterMessage { get; set; } }

public interface IInboxMessageProp<TKey, TPayload> { InboxMessage<TKey, TPayload>? InboxMessage { get; set; } }

public interface IInboxMessageErrorProp { string? InboxMessageError { get; set; } }

partial class DeadLetterFuncs
{
  static InboxMessage<TKey, TPayload> RequireInboxMessage<TKey, TPayload>(InboxMessage<TKey, TPayload>? inboxMessage) =>
    inboxMessage ?? throw new InvalidOperationException("Inbox message is required.");

  static string RequireInboxMessageError(string? error) =>
    error ?? throw new InvalidOperationException("Inbox message error is required.");
}