
namespace Kafka.Engine;

public interface IInsertInboxMessageService<TKey, TPayload>
{
  Task<bool> InsertInboxMessageAsync(InboxMessage<TKey, TPayload> message, CancellationToken ct = default);
}

public interface IStoreSessionModelService<TSession> where TSession: IDisposable {
  Task StoreSessionModelAsync<TModel>(TSession session, TModel model);
}

public interface IUpdateInboxMessageStatusService<TKey, TPayload>
{
  Task UpdateInboxMessageStatusAsync(
    InboxMessage<TKey, TPayload> message,
    InboxMessageStatus status,
    CancellationToken ct = default);
}

public interface IUpdateSessionInboxMessageStatusService<TKey, TPayload, TSession> where TSession: IDisposable
{
  Task UpdateSessionInboxMessageStatusAsync(
    TSession session,
    InboxMessage<TKey, TPayload> message,
    InboxMessageStatus status,
    CancellationToken ct = default);
}