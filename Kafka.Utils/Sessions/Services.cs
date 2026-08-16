
namespace Kafka.Utils;

public interface ISessionServices<TSession>:
  IStoreSessionService<TSession>,
  IStoreModelSessionService<TSession>,
  ITransactSessionService<TSession>
  where TSession : IDisposable;

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

