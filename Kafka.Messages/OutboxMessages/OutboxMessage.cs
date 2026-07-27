
namespace Kafka.Messages;

public record OutboxMessage<TKey, TContent> : MessageBase<TKey, TContent>
{
  public OutboxMessageStatus Status { get; init; } = OutboxMessageStatus.Pending;
}
