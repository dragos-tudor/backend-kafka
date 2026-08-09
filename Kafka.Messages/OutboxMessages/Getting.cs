
namespace Kafka.Messages;

partial class MessagesFuncs
{
  const int MaxOutboxRetries = 5;

  internal static OutboxMessageStatus GetOutboxMessageRetryStatus(
    int currentRetryCount,
    int maxRetries = MaxOutboxRetries) =>
      currentRetryCount + 1 < maxRetries
          ? OutboxMessageStatus.Pending
          : OutboxMessageStatus.DeadLettering;

  internal static OutboxMessageStatus GetOutboxDeadLetterRetryStatus(
    int currentRetryCount,
    int maxRetries = MaxOutboxRetries) =>
      currentRetryCount + 1 < maxRetries
          ? OutboxMessageStatus.DeadLettered
          : OutboxMessageStatus.Abandoned;

}