namespace Kafka.Resiliency;

partial class ResiliencyFuncs
{
  public static JobsOptions CreateJobsOptions(
    TimeSpan? resumeInboxInterval = default,
    TimeSpan? resumeInboxLockInterval = default,
    TimeSpan? relayOutboxInterval = default,
    TimeSpan? relayOutboxLockInterval = default)
    => new()
    {
      ResumeInboxInterval = resumeInboxInterval ?? TimeSpan.FromMinutes(1),
      ResumeInboxLockInterval = resumeInboxLockInterval ?? TimeSpan.FromSeconds(30),
      RelayOutboxInterval = relayOutboxInterval ?? TimeSpan.FromMinutes(1),
      RelayOutboxLockInterval = relayOutboxLockInterval ?? TimeSpan.FromSeconds(30)
    };

  public static JobsOptions CreateJobsOptionsFromEnvironment(
    string resumeInboxIntervalName = "KAFKA_RESUME_INBOX_INTERVAL_MS",
    string resumeInboxLockIntervalName = "KAFKA_RESUME_INBOX_LOCK_INTERVAL_MS",
    string relayOutboxIntervalName = "KAFKA_RELAY_OUTBOX_INTERVAL_MS",
    string relayOutboxLockIntervalName = "KAFKA_RELAY_OUTBOX_LOCK_INTERVAL_MS")
  {
    var resumeInboxInterval = TimeSpan.FromMilliseconds(ParseIntValue(Environment.GetEnvironmentVariable(resumeInboxIntervalName), 60000));
    var resumeInboxLockInterval = TimeSpan.FromMilliseconds(ParseIntValue(Environment.GetEnvironmentVariable(resumeInboxLockIntervalName), 60000));
    var relayOutboxInterval = TimeSpan.FromMilliseconds(ParseIntValue(Environment.GetEnvironmentVariable(relayOutboxIntervalName), 60000));
    var relayOutboxLockInterval = TimeSpan.FromMilliseconds(ParseIntValue(Environment.GetEnvironmentVariable(relayOutboxLockIntervalName), 60000));

    return CreateJobsOptions(
      resumeInboxInterval: resumeInboxInterval,
      resumeInboxLockInterval: resumeInboxLockInterval,
      relayOutboxInterval: relayOutboxInterval,
      relayOutboxLockInterval: relayOutboxLockInterval);
  }
}