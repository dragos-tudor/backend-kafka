#pragma warning disable CA1034

namespace Kafka.Messages;

public static class OutboxMessageExtenions
{
  extension<TKey, TPayload>(OutboxMessage<TKey, TPayload> message)
  {
    public OutboxMessage<TKey, TPayload> SetOutboxMessageStatus(OutboxMessageStatus status) { message.Status = status; return message; }
    public OutboxMessage<TKey, TPayload> SetOutboxMessageRetryCount(int retryCount) { message.RetryCount = retryCount; return message; }
    public OutboxMessage<TKey, TPayload> SetOutboxMessageDispatchRetryCount(int retryCount) { message.DispatchRetryCount = retryCount; return message; }
    public OutboxMessage<TKey, TPayload> SetOutboxMessageLastError(string error) { message.LastError = error; return message; }
    public OutboxMessage<TKey, TPayload> SetOutboxMessageNextAttemptAt(DateTimeOffset? nextAttemptAt) { message.NextAttemptAt = nextAttemptAt; return message; }
  }
}