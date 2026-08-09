
namespace Kafka.Messages;

partial class MessagesFuncs
{
  const int MaxInboxRetries = 5;

  internal static InboxMessageStatus GetInboxMessageRetryStatus(
    int currentRetryCount,
    int maxRetries = MaxInboxRetries) =>
      currentRetryCount + 1 < maxRetries
          ? InboxMessageStatus.Pending
          : InboxMessageStatus.DeadLettering;

  internal static InboxMessageStatus GetInboxDeadLetterRetryStatus(
    int currentRetryCount,
    int maxRetries = MaxInboxRetries) =>
      currentRetryCount + 1 < maxRetries
          ? InboxMessageStatus.DeadLettered
          : InboxMessageStatus.Abandoned;

}