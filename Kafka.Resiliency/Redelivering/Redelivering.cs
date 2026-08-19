
namespace Kafka.Resiliency;

partial class ResiliencyFuncs
{
  internal static Task RedeliverDeadLetterMessagesJobAsync<TKey, TValue, TPayload>(
    RedeliverJobsOptions options,
    IRetryDeadLetterMessagesServices<TKey, TValue, TPayload> services,
    CancellationToken cancellationToken = default)
  {
    return RunPeriodicJobAsync(
      "redeliver.dead.letter.messages",
      options.RedeliverDeadLetterInterval,
      options.RedeliverDeadLetterLockInterval,
      ct => RedeliverDeadLetterMessagesAsync<IRedeliveringServices<TKey, TValue, TPayload>, IRedeliveringData<TKey, TValue, TPayload>, TKey, TValue, TPayload>(services, ct),
      services,
      cancellationToken);
  }
}
