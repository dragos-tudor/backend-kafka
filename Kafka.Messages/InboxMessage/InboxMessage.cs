
namespace Kafka.Messages;

public record InboxMessage<TKey, TPayload> : IntegrationMessage<TKey, TPayload>
{
  public DateTime ReceivedAt { get; init; }
  public InboxMessageStatus Status { get; set; } = InboxMessageStatus.Pending;
}
