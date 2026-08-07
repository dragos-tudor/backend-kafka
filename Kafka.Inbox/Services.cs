
namespace Kafka.Inbox;

public interface IHandleInboxMessageService<TKey, TPayload>
{
  Task<Result<object?, string?>> HandleInboxMessageAsync(InboxMessage<TKey, TPayload> message, CancellationToken ct = default);
}

public interface IScheduleOptionsService { RetryMessageOptions GetScheduleRetryOptions(); }

public interface IDelayOptionsService { RetryMessageOptions GetDelayRetryOptions(); }

public interface IStoreSessionService<TSession> where TSession : IDisposable { TSession GetSession(); }

public interface IStoreModelSessionService<TSession> where TSession: IDisposable {
  Task StoreModelAsync<TModel>(TSession session, TModel model);
}

public interface ITransactSessionService<TSession> where TSession: IDisposable {
  Task TransactSessionAsync(
    TSession session,
    Func<TSession, Task> func1,
    Func<TSession, Task> func2,
    CancellationToken ct = default
  );
}

