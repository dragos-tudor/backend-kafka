
namespace Kafka.Engine;

public interface IHandleInboxMessageService<TKey, TPayload>
{
  Task<Result<object?, string?>> HandleInboxMessageAsync(InboxMessage<TKey, TPayload> message, CancellationToken ct = default);
}