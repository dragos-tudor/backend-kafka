
namespace Kafka.Messages;


partial class MessagesFuncs
{
  const int MaxHandleRetries = 5;
  const int MaxPublishRetries = 5;

  internal static InboxMessageStatus GetHandledInboxMessageStatus<T, TError>(
    T? model,
    TError? error)
  =>
    model is not null?
      InboxMessageStatus.Handled:
      InboxMessageStatus.DeadLettering;

  internal static InboxMessageStatus GetRetryInboxMessageStatus(
    int currentRetryCount,
    int maxHandleRetries = MaxHandleRetries)
  {
    var nextRetryCount = currentRetryCount + 1;
    return nextRetryCount > maxHandleRetries
        ? InboxMessageStatus.DeadLettering
        : InboxMessageStatus.Pending;
  }

  internal static InboxMessageStatus GetPublishInboxMessageStatus(
    string error,
    int currentRetryCount,
    int maxPublishRetries = MaxPublishRetries)
  {
    if (error is null) return InboxMessageStatus.DeadLettered;

    var nextRetryCount = currentRetryCount + 1;
    return nextRetryCount > maxPublishRetries
        ? InboxMessageStatus.Abandoned
        : InboxMessageStatus.DeadLettering;
  }
}