
namespace Kafka.Messages;

public record OutboxMessage<TKey, TContent> : PersistedMessage<TKey, TContent>
{
  public OutboxMessageStatus Status { get; init; } = OutboxMessageStatus.Pending;
}
