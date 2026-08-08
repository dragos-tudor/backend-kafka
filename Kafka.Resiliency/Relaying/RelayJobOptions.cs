
namespace Kafka.Resiliency;

public record RelayJobsOptions
{
  public TimeSpan RelayOutboxInterval { get; init; }
  public TimeSpan RelayOutboxLockInterval { get; init; }
}

partial class ResiliencyFuncs
{
  public static RelayJobsOptions CreateRelayJobsOptions(
    TimeSpan? relayOutboxInterval = default,
    TimeSpan? relayOutboxLockInterval = default)
    => new()
    {
      RelayOutboxInterval = relayOutboxInterval ?? TimeSpan.FromMinutes(1),
      RelayOutboxLockInterval = relayOutboxLockInterval ?? TimeSpan.FromSeconds(30)
    };

  public static RelayJobsOptions CreateRelayJobsOptionsFromEnvironment(
    string relayOutboxIntervalName = "KAFKA_RELAY_OUTBOX_INTERVAL_MS",
    string relayOutboxLockIntervalName = "KAFKA_RELAY_OUTBOX_LOCK_INTERVAL_MS")
  {
    var relayOutboxInterval = TimeSpan.FromMilliseconds(ParseIntValue(Environment.GetEnvironmentVariable(relayOutboxIntervalName), 60000));
    var relayOutboxLockInterval = TimeSpan.FromMilliseconds(ParseIntValue(Environment.GetEnvironmentVariable(relayOutboxLockIntervalName), 60000));

    return CreateRelayJobsOptions(
      relayOutboxInterval: relayOutboxInterval,
      relayOutboxLockInterval: relayOutboxLockInterval);
  }
}