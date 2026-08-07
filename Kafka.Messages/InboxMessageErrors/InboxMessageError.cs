
namespace Kafka.Messages;

public record InboxMessageError
{
  public required InboxMessageStatus Status { get; init; }
  public required int RetryCount { get; init; }
  public DateTimeOffset? NextAttemptAt { get; init; }
  public string? LastError { get; init; }
}
