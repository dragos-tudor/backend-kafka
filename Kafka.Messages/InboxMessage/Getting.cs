
namespace Kafka.Messages;

partial class MessagesFuncs
{
  const int MaxInboxRetries = 5;

  internal static InboxMessageStatus GetInboxMessageStatus(
    int currentRetryCount,
    int maxRetries = MaxInboxRetries) =>
      currentRetryCount + 1 <= maxRetries
          ? InboxMessageStatus.Processing
          : InboxMessageStatus.DeadLettering;

  internal static string GetInboxMessageValidationErrors<TKey, TPayload>(InboxMessage<TKey, TPayload> message) =>
    string.Join(
      Environment.NewLine,
      ValidateInboxMessage(message));
}