
namespace Kafka.Resiliency;

public interface IKafkaServices<TKey, TValue, TPayload, TSession> :
  IConsumingServices<TKey, TValue, TPayload, TSession>,
  IHandlingServices<TKey, TValue, TPayload, TSession>,
  IRetryInboxMessagesServices<TKey, TValue, TPayload, TSession>,
  IResumingServices<TKey, TValue, TPayload, TSession>,
  IRetryOutboxMessagesServices<TKey, TValue, TPayload>,
  IRelayOutboxMessagesServices<TKey, TValue, TPayload>,
  IRunPeriodicJobServices where TSession: IDisposable;

public interface IRunPeriodicJobServices :
  IDistributedLockService,
  ILoggerService;

public interface IRetryInboxMessagesServices<TKey, TValue, TPayload, TSession> :
  IRunPeriodicJobServices,
  IResumingServices<TKey, TValue, TPayload, TSession> where TSession : IDisposable;

public interface IRetryOutboxMessagesServices<TKey, TValue, TPayload> :
  IRunPeriodicJobServices,
  IRelayOutboxMessagesServices<TKey, TValue, TPayload>;

public interface IDistributedLockService
{
  Task<IAsyncDisposable?> TryAcquireLockAsync(string key, TimeSpan lockDuration, CancellationToken cancellationToken);
}