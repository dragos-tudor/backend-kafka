
namespace Kafka.Resiliency;

partial class ResiliencyFuncs
{
  internal static Task RetryInboxMessagesAsync<TKey, TValue, TPayload, TSession>(
    JobsOptions options,
    IRetryInboxMessagesServices<TKey, TValue, TPayload, TSession> services,
    CancellationToken cancellationToken = default)
  where TSession : IDisposable =>
      RunPeriodicJobAsync(
        "resume-inbox-messages",
        options.ResumeInboxInterval,
        options.ResumeInboxLockInterval,
        token => ResumeInboxMessagesAsync<IResumeInboxMessageServices<TKey, TValue, TPayload, TSession>, IResumingStepData<TKey, TValue, TPayload>, TKey, TValue, TPayload, TSession>(services, token),
        services,
        cancellationToken);

  internal static Task RetryOutboxMessagesAsync<TKey, TValue, TPayload>(
    JobsOptions options,
    IRetryOutboxMessagesServices<TKey, TValue, TPayload> services,
    CancellationToken cancellationToken = default) =>
      RunPeriodicJobAsync(
        "relay-outbox-messages",
        options.RelayOutboxInterval,
        options.RelayOutboxLockInterval,
        token => RelayOutboxMessagesAsync(services, token),
        services,
        cancellationToken);
}
