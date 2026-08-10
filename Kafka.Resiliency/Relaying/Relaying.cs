
namespace Kafka.Resiliency;

partial class ResiliencyFuncs
{
  internal static Task RelayOutboxMessagesJobAsync<TKey, TValue, TPayload>(
    RelayJobsOptions options,
    IRetryOutboxMessagesServices<TKey, TValue, TPayload> services,
    CancellationToken cancellationToken = default)
  {
    using var producer = services.GetProducer(PipelineType.Relaying.ToString(), true);
    var relaying = RelayOutboxMessagesAsync<
      IRelayingServices<TKey, TValue, TPayload>,
      IRelayingData<TKey, TValue, TPayload>,
      TKey, TValue, TPayload>;
    return RunPeriodicJobAsync(
      "relay.outbox.messages",
      options.RelayOutboxInterval,
      options.RelayOutboxLockInterval,
      ct => relaying(services, ct),
      services,
      cancellationToken);
  }
}
