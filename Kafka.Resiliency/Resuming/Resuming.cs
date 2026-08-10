
namespace Kafka.Resiliency;

partial class ResiliencyFuncs
{
  internal static Task ResumeInboxMessagesJobAsync<TKey, TValue, TPayload, TSession>(
    ResumeJobsOptions options,
    IRetryInboxMessagesServices<TKey, TValue, TPayload, TSession> services,
    CancellationToken cancellationToken = default)
  where TSession : IDisposable =>
      RunPeriodicJobAsync(
        "resume.inbox.messages",
        options.ResumeInboxInterval,
        options.ResumeInboxLockInterval,
        ct => ResumeInboxMessagesAsync<IResumingServices<TKey, TValue, TPayload, TSession>, IResumingData<TKey, TValue, TPayload>, TKey, TValue, TPayload, TSession>(services, ct),
        services,
        cancellationToken);
}
