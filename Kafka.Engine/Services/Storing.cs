
namespace Kafka.Engine;

public interface IInsertInboxMessageService<TKey, TPayload>
{
  Task<bool> InsertInboxMessageAsync(InboxMessage<TKey, TPayload> message, CancellationToken ct = default);
}

public interface ITransactInboxService<TSession> where TSession: IDisposable {
  Task TransactInboxAsync<T1, T2>(
    TSession session,
    Func<TSession, T1> func1,
    Func<TSession, T2> func2,
    CancellationToken ct = default
  );
}

public interface IStoreSessionModelService<TSession> where TSession: IDisposable {
  Task StoreSessionModel<TModel>(TSession session, TModel model);
}

public interface IUpdateInboxMessageStatusService<TKey, TPayload>
{
  Task UpdateInboxMessageStatusAsync(
    InboxMessage<TKey, TPayload> message,
    InboxMessageStatus status,
    CancellationToken ct = default);
}

public interface IUpdateSessionInboxMessageStatusService<TSession, TKey, TPayload> where TSession: IDisposable
{
  Task UpdateSessionInboxMessageStatus(
    TSession session,
    InboxMessage<TKey, TPayload> message,
    InboxMessageStatus status);
}