
namespace Kafka.Resiliency;

public interface IKafkaServices<TKey, TValue, TPayload, TSession> :
  IConsumingServices<TKey, TValue, TPayload, TSession>,
  IResumingServices<TKey, TValue, TPayload, TSession>,
  IRetryDeadLetterMessagesServices<TKey, TValue, TPayload>,
  IRelayingServices<TKey, TValue, TPayload>,
  IRetryInboxMessagesServices<TKey, TValue, TPayload, TSession>,
  IRetryOutboxMessagesServices<TKey, TValue, TPayload>,
  IRunPeriodicJobServices where TSession: IDisposable;

public interface IRunPeriodicJobServices :
  IDistributedLockService,
  ILoggerService;

public interface IRetryInboxMessagesServices<TKey, TValue, TPayload, TSession> :
  IRunPeriodicJobServices,
  IResumingServices<TKey, TValue, TPayload, TSession> where TSession : IDisposable;

public interface IRetryOutboxMessagesServices<TKey, TValue, TPayload> :
  IRunPeriodicJobServices,
  IRelayingServices<TKey, TValue, TPayload>;

public interface IRetryDeadLetterMessagesServices<TKey, TValue, TPayload> :
  IRunPeriodicJobServices,
  IRedeliveringServices<TKey, TValue, TPayload>;

public interface IDistributedLockService
{
  Task<IAsyncDisposable?> TryAcquireLockAsync(string key, TimeSpan lockDuration, CancellationToken cancellationToken);
}