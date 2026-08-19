
namespace Kafka.Resiliency;

partial class ResiliencyFuncs
{
  internal static Task ResumeInboxMessagesJobAsync<TKey, TValue, TPayload, TSession>(
    ResumeJobsOptions options,
    IRetryInboxMessagesServices<TKey, TValue, TPayload, TSession> services,
    CancellationToken cancellationToken = default)
  where TSession : IDisposable
  {
    return RunPeriodicJobAsync(
      "resume.inbox.messages",
      options.ResumeInboxInterval,
      options.ResumeInboxLockInterval,
      ct => ResumeInboxMessagesAsync<IResumingServices<TKey, TPayload, TSession>, IResumingData<TKey, TPayload>, TKey, TPayload, TSession>(services, ct),
      services,
      cancellationToken);
  }
}
