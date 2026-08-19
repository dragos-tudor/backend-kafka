
namespace Kafka.Resiliency;

public record DeadLetterJobsOptions
{
  public TimeSpan DeadLetterInboxInterval { get; init; }
  public TimeSpan DeadLetterInboxLockInterval { get; init; }
}

partial class ResiliencyFuncs
{
  public static DeadLetterJobsOptions CreateDeadLetterJobsOptions(
    TimeSpan? deadLetterInboxInterval = default,
    TimeSpan? deadLetterInboxLockInterval = default)
    => new()
    {
      DeadLetterInboxInterval = deadLetterInboxInterval ?? TimeSpan.FromMinutes(1),
      DeadLetterInboxLockInterval = deadLetterInboxLockInterval ?? TimeSpan.FromSeconds(30)
    };

  public static DeadLetterJobsOptions CreateDeadLetterJobsOptionsFromEnvironment(
    string deadLetterInboxIntervalName = "KAFKA_DEADLETTER_INBOX_INTERVAL_MS",
    string deadLetterInboxLockIntervalName = "KAFKA_DEADLETTER_INBOX_LOCK_INTERVAL_MS")
  {
    var deadLetterInboxInterval = TimeSpan.FromMilliseconds(ParseIntValue(Environment.GetEnvironmentVariable(deadLetterInboxIntervalName), 60000));
    var deadLetterInboxLockInterval = TimeSpan.FromMilliseconds(ParseIntValue(Environment.GetEnvironmentVariable(deadLetterInboxLockIntervalName), 60000));

    return CreateDeadLetterJobsOptions(
      deadLetterInboxInterval: deadLetterInboxInterval,
      deadLetterInboxLockInterval: deadLetterInboxLockInterval);
  }
}
