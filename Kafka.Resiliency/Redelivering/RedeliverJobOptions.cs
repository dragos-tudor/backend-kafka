
namespace Kafka.Resiliency;

public record RedeliverJobsOptions
{
  public TimeSpan RedeliverDeadLetterInterval { get; init; }
  public TimeSpan RedeliverDeadLetterLockInterval { get; init; }
}

partial class ResiliencyFuncs
{
  public static RedeliverJobsOptions CreateRedeliverJobsOptions(
    TimeSpan? redeliverDeadLetterInterval = default,
    TimeSpan? redeliverDeadLetterLockInterval = default)
    => new()
    {
      RedeliverDeadLetterInterval = redeliverDeadLetterInterval ?? TimeSpan.FromMinutes(1),
      RedeliverDeadLetterLockInterval = redeliverDeadLetterLockInterval ?? TimeSpan.FromSeconds(30)
    };

  public static RedeliverJobsOptions CreateRedeliverJobsOptionsFromEnvironment(
    string redeliverDeadLetterIntervalName = "KAFKA_REDELIVER_DEAD_LETTER_INTERVAL_MS",
    string redeliverDeadLetterLockIntervalName = "KAFKA_REDELIVER_DEAD_LETTER_LOCK_INTERVAL_MS")
  {
    var redeliverDeadLetterInterval = TimeSpan.FromMilliseconds(ParseIntValue(Environment.GetEnvironmentVariable(redeliverDeadLetterIntervalName), 60000));
    var redeliverDeadLetterLockInterval = TimeSpan.FromMilliseconds(ParseIntValue(Environment.GetEnvironmentVariable(redeliverDeadLetterLockIntervalName), 60000));

    return CreateRedeliverJobsOptions(
      redeliverDeadLetterInterval: redeliverDeadLetterInterval,
      redeliverDeadLetterLockInterval: redeliverDeadLetterLockInterval);
  }
}