
namespace Kafka.Messages;

partial class MessagesFuncs
{
  const int MaxOutboxRetries = 5;

  internal static OutboxMessageStatus GetOutboxMessageRetryStatus(
    int currentRetryCount,
    int maxRetries = MaxOutboxRetries) =>
      currentRetryCount + 1 < maxRetries
          ? OutboxMessageStatus.Pending
          : OutboxMessageStatus.Dispatching;

  internal static OutboxMessageStatus GetOutboxDeadLetterRetryStatus(
    int currentRetryCount,
    int maxRetries = MaxOutboxRetries) =>
      currentRetryCount + 1 < maxRetries
          ? OutboxMessageStatus.Dispatching
          : OutboxMessageStatus.Abandoned;

}