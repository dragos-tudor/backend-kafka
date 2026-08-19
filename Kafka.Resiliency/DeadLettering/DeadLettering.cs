
namespace Kafka.Resiliency;

partial class ResiliencyFuncs
{
  internal static Task DeadLetterInboxMessagesJobAsync<TKey, TValue, TPayload>(
    DeadLetterJobsOptions options,
    IRetryDeadLetteringInboxMessagesServices<TKey, TPayload> services,
    CancellationToken cancellationToken = default)
  {
    return RunPeriodicJobAsync(
      "deadletter.inbox.messages",
      options.DeadLetterInboxInterval,
      options.DeadLetterInboxLockInterval,
      ct => DeadLetterInboxMessagesAsync<IRetryDeadLetteringInboxMessagesServices<TKey, TPayload>, IDeadLetteringData<TKey, TPayload>, TKey, TPayload>(services, ct),
      services,
      cancellationToken);
  }
}
