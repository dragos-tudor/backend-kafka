
namespace Kafka.Messages;

public record InboxMessage<TKey, TPayload> : PersistedMessage<TKey, TPayload>
{
  public DateTime ReceivedAt { get; init; }
  public int? PublishRetryCount { get; set; }
  public InboxMessageStatus Status { get; set; } = InboxMessageStatus.Pending;
}
