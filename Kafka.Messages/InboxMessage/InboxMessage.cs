
namespace Kafka.Messages;

public record InboxMessage<TKey, TPayload> : MessageBase<TKey, TPayload>
{
  public InboxMessageStatus Status { get; init; } = InboxMessageStatus.Pending;
}
