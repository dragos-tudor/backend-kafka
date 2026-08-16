
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
      ct => ResumeInboxMessagesAsync(services, ct),
      services,
      cancellationToken);
  }
}
