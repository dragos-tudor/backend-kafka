#pragma warning disable CA1034

namespace Kafka.Messages;

public static class InboxMessageExtenions
{
  extension<TKey, TPayload>(InboxMessage<TKey, TPayload> message)
  {
    public InboxMessage<TKey, TPayload> SetInboxMessageStatus(InboxMessageStatus status) { message.Status = status; return message; }
    public InboxMessage<TKey, TPayload> SetInboxMessageRetryCount(int retryCount) { message.RetryCount = retryCount; return message; }
    public InboxMessage<TKey, TPayload> SetInboxMessageDispatchRetryCount(int retryCount) { message.DispatchRetryCount = retryCount; return message; }
    public InboxMessage<TKey, TPayload> SetInboxMessageLastError(string error) { message.LastError = error; return message; }
    public InboxMessage<TKey, TPayload> SetInboxMessageNextAttemptAt(DateTimeOffset? nextAttemptAt) { message.NextAttemptAt = nextAttemptAt; return message; }
  }
}