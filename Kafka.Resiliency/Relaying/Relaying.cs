
namespace Kafka.Resiliency;

partial class ResiliencyFuncs
{
  internal static Task RelayOutboxMessagesJobAsync<TKey, TValue, TPayload>(
    RelayJobsOptions options,
    IRetryOutboxMessagesServices<TKey, TValue, TPayload> services,
    CancellationToken cancellationToken = default)
  {
    return RunPeriodicJobAsync(
      "relay.outbox.messages",
      options.RelayOutboxInterval,
      options.RelayOutboxLockInterval,
      ct => RelayOutboxMessagesAsync(services, ct),
      services,
      cancellationToken);
  }
}
