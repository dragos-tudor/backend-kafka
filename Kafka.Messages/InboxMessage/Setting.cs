
namespace Kafka.Messages;

partial class MessagesFuncs
{
  public static InboxMessage<TKey, TPayload> SetInboxMessageRetryCount<TKey, TPayload>(this InboxMessage<TKey, TPayload> message, int retryCount)
    { message.RetryCount = retryCount; return message; }

  public static InboxMessage<TKey, TPayload> SetInboxMessageLastError<TKey, TPayload>(this InboxMessage<TKey, TPayload> message, string error)
    { message.LastError = error; return message; }

  public static InboxMessage<TKey, TPayload> SetInboxMessageNextAttemptAt<TKey, TPayload>(this InboxMessage<TKey, TPayload> message, DateTimeOffset? nextAttemptAt)
    { message.NextAttemptAt = nextAttemptAt; return message; }

  public static InboxMessage<TKey, TPayload> SetInboxMessageStatus<TKey, TPayload>(InboxMessage<TKey, TPayload> message, InboxMessageStatus status)
    { message.Status = status; return message; }
}