
namespace Kafka.Engine;

public interface IGetStoreSession<TSession> where TSession : IDisposable { TSession GetSession(); }

public interface IInsertInboxMessage<TKey, TPayload>
{
  Task<bool> InsertInboxMessageAsync(InboxMessage<TKey, TPayload> message, CancellationToken ct = default);
}

public interface IStoreSessionModel<TSession> where TSession: IDisposable {
  Task StoreSessionModel<TModel>(TSession session, TModel model);
}

public interface ITransactInbox<TSession> where TSession: IDisposable {
  Task TransactInboxAsync<T1, T2>(
    TSession session,
    Func<TSession, T1> func1,
    Func<TSession, T2> func2,
    CancellationToken ct = default
  );
}

public interface IUpdateInboxMessageStatus<TKey, TPayload>
{
  Task UpdateInboxMessageStatusAsync(
    InboxMessage<TKey, TPayload> message,
    InboxMessageStatus status,
    CancellationToken ct = default);
}

public interface IUpdateSessionInboxMessageStatus<TSession, TKey, TPayload> where TSession: IDisposable
{
  Task UpdateSessionInboxMessageStatus(
    TSession session,
    InboxMessage<TKey, TPayload> message,
    InboxMessageStatus status);
}