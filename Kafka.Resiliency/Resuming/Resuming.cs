
namespace Kafka.Resiliency;

partial class ResiliencyFuncs
{
  internal static Task ResumeInboxMessagesJobAsync<TKey, TValue, TPayload, TSession>(
    ResumeJobsOptions options,
    IRetryInboxMessagesServices<TKey, TValue, TPayload, TSession> services,
    CancellationToken cancellationToken = default)
  where TSession : IDisposable
  {
    using var producer = services.GetProducer(PipelineType.Resuming.ToString(), true);
    var resuming = ResumeInboxMessagesAsync<
      IResumingServices<TKey, TValue, TPayload, TSession>,
      IResumingData<TKey, TValue, TPayload>,
      TKey, TValue, TPayload, TSession>;
    return RunPeriodicJobAsync(
      "resume.inbox.messages",
      options.ResumeInboxInterval,
      options.ResumeInboxLockInterval,
      ct => resuming(services, ct),
      services,
      cancellationToken);
  }
}
