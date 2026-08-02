
namespace Kafka.Resiliency;

public interface IKafkaServices<TKey, TValue, TPayload, TSession> :
  IConsumeKafkaMessages<TKey, TValue, TPayload, TSession>,
  IConsumeKafkaMessage<TKey, TValue, TPayload, TSession>,
  IHandleInboxMessage<TKey, TValue, TPayload, TSession>,
  IRetryInboxMessagesServices<TKey, TValue, TPayload, TSession>,
  IResumeInboxMessagesServices<TKey, TValue, TPayload, TSession>,
  IRetryOutboxMessagesServices<TKey, TValue, TPayload>,
  IRelayOutboxMessagesServices<TKey, TValue, TPayload>,
  IRunKafkaMessagesServices<TKey, TValue, TPayload, TSession>,
  IRunPeriodicJobServices where TSession: IDisposable;

public interface IRunKafkaMessagesServices<TKey, TValue, TPayload, TSession> :
  IConsumeKafkaMessages<TKey, TValue, TPayload, TSession>,
  IGetLogger where TSession: IDisposable;

public interface IRunPeriodicJobServices :
  IDistributedLockService,
  IGetLogger;

public interface IRetryInboxMessagesServices<TKey, TValue, TPayload, TSession> :
  IRunPeriodicJobServices,
  IResumeInboxMessagesServices<TKey, TValue, TPayload, TSession> where TSession : IDisposable;

public interface IRetryOutboxMessagesServices<TKey, TValue, TPayload> :
  IRunPeriodicJobServices,
  IRelayOutboxMessagesServices<TKey, TValue, TPayload>;