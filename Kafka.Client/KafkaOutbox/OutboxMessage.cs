
namespace Kafka.Client;

public enum OutboxMessageStatus { Pending, Sent, Failed, DeadLettered }

public record OutboxMessage : Message
{
  public required string Topic { get; init; }
  public string? Key { get; init; }
  public OutboxMessageStatus Status { get; init; } = OutboxMessageStatus.Pending;
  public int PublishAttemptCount { get; init; }
  public string? LastFailureReason { get; init; }
  public string? MessageMetadata { get; init; } // JSON snapshot of topic/partition/offset, populated after a successful publish
}
