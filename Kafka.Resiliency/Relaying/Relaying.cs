
namespace Kafka.Resiliency;

partial class ResiliencyFuncs
{
  internal static Task RelayOutboxMessagesJobAsync<TKey, TValue, TPayload>(
    RelayJobsOptions options,
    IRetryOutboxMessagesServices<TKey, TValue, TPayload> services,
    CancellationToken cancellationToken = default) =>
      RunPeriodicJobAsync(
        "relay.outbox.messages",
        options.RelayOutboxInterval,
        options.RelayOutboxLockInterval,
        ct => RelayOutboxMessagesAsync<IRelayOutboxMessagesServices<TKey, TValue, TPayload>, IRelayingData<TKey, TValue, TPayload>, TKey, TValue, TPayload>(services, ct),
        services,
        cancellationToken);
}
