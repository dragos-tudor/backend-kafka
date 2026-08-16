using static Kafka.Messages.MessageFieldConstraints;

namespace Kafka.Messages;

public record InboxMessage<TKey, TPayload>
{
  public required Guid MessageId { get; init; }
  public required TKey MessageKey { get; init; }
  public TPayload? Payload { get; init; }
  public required DateTime Date { get; init; }
  public InboxMessageStatus Status { get; set; } = InboxMessageStatus.Processing;
  public DateTime ReceivedAt { get; init; } = DateTime.UtcNow;
  [MaxLength(TypeMaxLength)]
  public string? Type { get; init; }
  public int? Version { get; init; } = 1;
  [MaxLength(MetadataMaxLength)]
  public string? Metadata { get; init; }
  public Guid? CorrelationId { get; init; }
  public int? RetryCount { get; set; }
  public DateTimeOffset? NextAttemptAt { get; set; }
  [MaxLength(LastErrorMaxLength)]
  public string? LastError { get; set; }
}
