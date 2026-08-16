
namespace Kafka.Messages;

partial class MessagesFuncs
{
  const int MaxOutboxRetries = 5;

  internal static OutboxMessageStatus GetOutboxMessageStatus(
    int currentRetryCount,
    int maxRetries = MaxOutboxRetries) =>
      currentRetryCount + 1 <= maxRetries
          ? OutboxMessageStatus.Pending
          : OutboxMessageStatus.Abandoned;

  internal static string GetOutboxMessageValidationErrors<TKey, TPayload>(OutboxMessage<TKey, TPayload> message) =>
    string.Join(
      Environment.NewLine,
      ValidateOutboxMessage(message));
}