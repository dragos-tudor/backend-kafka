
namespace Kafka.Messages;

public abstract record IntegrationMessage<TKey, TPayload>
{
  public required Guid MessageId { get; init; } = Guid.NewGuid();
  public required TKey MessageKey { get; init; }
  public TPayload? Payload { get; init; }
  public required DateTime Date { get; init; } = DateTime.UtcNow;
  public string? Type { get; init; }
  public int? Version { get; init; } = 1;
  public string? Metadata { get; init; }
  public Guid? CorrelationId { get; init; }
  public int? RetryCount { get; set; }
  public DateTimeOffset? NextAttemptAt { get; set; }
  public string? LastError { get; set; }
  public int? DispatchRetryCount { get; set; }
}
