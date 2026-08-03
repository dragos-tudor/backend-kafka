
namespace Kafka.Engine;

public interface IGetStoreSessionService<TSession> where TSession : IDisposable { TSession GetSession(); }

public interface ITransactSessionService<TSession> where TSession: IDisposable {
  Task TransactSessionAsync(
    TSession session,
    Func<TSession, Task> func1,
    Func<TSession, Task> func2,
    CancellationToken ct = default
  );
}