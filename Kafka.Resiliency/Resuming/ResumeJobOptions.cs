
namespace Kafka.Resiliency;

public record ResumeJobsOptions
{
  public TimeSpan ResumeInboxInterval { get; init; }
  public TimeSpan ResumeInboxLockInterval { get; init; }
}

partial class ResiliencyFuncs
{
  public static ResumeJobsOptions CreateResumeJobsOptions(
    TimeSpan? resumeInboxInterval = default,
    TimeSpan? resumeInboxLockInterval = default)
    => new()
    {
      ResumeInboxInterval = resumeInboxInterval ?? TimeSpan.FromMinutes(1),
      ResumeInboxLockInterval = resumeInboxLockInterval ?? TimeSpan.FromSeconds(30),
    };

  public static ResumeJobsOptions CreateResumeJobsOptionsFromEnvironment(
    string resumeInboxIntervalName = "KAFKA_RESUME_INBOX_INTERVAL_MS",
    string resumeInboxLockIntervalName = "KAFKA_RESUME_INBOX_LOCK_INTERVAL_MS")
  {
    var resumeInboxInterval = TimeSpan.FromMilliseconds(ParseIntValue(Environment.GetEnvironmentVariable(resumeInboxIntervalName), 60000));
    var resumeInboxLockInterval = TimeSpan.FromMilliseconds(ParseIntValue(Environment.GetEnvironmentVariable(resumeInboxLockIntervalName), 60000));

    return CreateResumeJobsOptions(
      resumeInboxInterval,
      resumeInboxLockInterval);
  }
}