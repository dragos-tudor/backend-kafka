
namespace Kafka.Messages;

public record OutboxMessage<TKey, TContent> : IntegrationMessage<TKey, TContent>
{
  public OutboxMessageStatus Status { get; set; } = OutboxMessageStatus.Pending;
}
