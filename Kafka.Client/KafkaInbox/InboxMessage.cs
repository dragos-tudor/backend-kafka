
namespace Kafka.Client;

public enum InboxMessageStatus { Pending, Processing, Succeeded, Retry, DeadLettered }

public record InboxMessage : Message
{
  public required string Topic { get; init; }
  public required int Partition { get; init; }
  public required long Offset { get; init; }
  public InboxMessageStatus Status { get; init; } = InboxMessageStatus.Pending;
  public int RetryCount { get; init; }
  public DateTime? NextAttemptAt { get; init; }
  public string? LastFailureReason { get; init; }
}
