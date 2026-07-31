
namespace Kafka.Resiliency;

public record JobsOptions
{
  public TimeSpan ResumeInboxInterval { get; init; }
  public TimeSpan ResumeInboxLockInterval { get; init; }
  public TimeSpan RelayOutboxInterval { get; init; }
  public TimeSpan RelayOutboxLockInterval { get; init; }
}