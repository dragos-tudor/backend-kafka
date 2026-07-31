
namespace Kafka.Engine;

public interface IConsumeKafkaMessagesServices<TKey, TValue, TPayload, TSession> :
  IConsumeKafkaMessageServices<TKey, TValue, TPayload, TSession> where TSession : IDisposable;

public interface IConsumeKafkaMessageServices<TKey, TValue, TPayload, TSession> :
  IGetLoggerService,
  IHandleInboxMessageServices<TKey, TValue, TPayload, TSession>,
  IInsertInboxMessageServices<TKey, TValue, TPayload>,
  IPublishKafkaDeadLetterServices<TKey, TValue, TPayload> where TSession : IDisposable;

public interface IHandleInboxMessageServices<TKey, TValue, TPayload, TSession> :
  IGetSessionService<TSession>,
  IHandleInboxMessageService<TKey, TPayload>,
  IStoreSessionModelService<TSession>,
  ITransactInboxService<TSession>,
  IUpdateSessionInboxMessageStatusService<TSession, TKey, TPayload> where TSession : IDisposable;

public interface IInsertInboxMessageServices<TKey, TValue, TPayload> :
  IGetUtcDateService,
  IInsertInboxMessageService<TKey, TPayload>,
  IPersistedMessagePayloadService<TValue, TPayload>;

public interface IPublishKafkaDeadLetterServices<TKey, TValue, TPayload> :
  IGetDeadLetterTopicService<TKey, TPayload>,
  IGetUtcDateService,
  IKafkaMessageValueService<TPayload, TValue>,
  IUpdateInboxMessageStatusService<TKey, TPayload>;

public interface IRelayOutboxMessagesServices<TKey, TValue, TPaylod> :
  IGetLoggerService;

public interface IResumeInboxMessagesServices<TKey, TValue, TPayload, TSession> :
  IGetLoggerService where TSession : IDisposable;