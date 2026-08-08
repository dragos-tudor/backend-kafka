
namespace Kafka.Resiliency;

partial class ResiliencyFuncs
{
  internal static Task RetryInboxMessagesAsync<TKey, TValue, TPayload, TSession>(
    JobsOptions options,
    IRetryInboxMessagesServices<TKey, TValue, TPayload, TSession> services,
    CancellationToken cancellationToken = default)
  where TSession : IDisposable =>
      RunPeriodicJobAsync(
        "resume.inbox.messages",
        options.ResumeInboxInterval,
        options.ResumeInboxLockInterval,
        ct => ResumeInboxMessagesAsync<IResumeInboxMessageServices<TKey, TValue, TPayload, TSession>, IResumingStepData<TKey, TValue, TPayload>, TKey, TValue, TPayload, TSession>(services, ct),
        services,
        cancellationToken);

  internal static Task RetryOutboxMessagesAsync<TKey, TValue, TPayload>(
    JobsOptions options,
    IRetryOutboxMessagesServices<TKey, TValue, TPayload> services,
    CancellationToken cancellationToken = default) =>
      RunPeriodicJobAsync(
        "relay.outbox.messages",
        options.RelayOutboxInterval,
        options.RelayOutboxLockInterval,
        ct => RelayOutboxMessagesAsync<IRelayOutboxMessagesServices<TKey, TValue, TPayload>, IRelayingStepData<TKey, TValue, TPayload>, TKey, TValue, TPayload>(services, ct),
        services,
        cancellationToken);
}
