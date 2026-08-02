
namespace Kafka.Engine;

public interface IConsumeKafkaMessages<TKey, TValue, TPayload, TSession> :
  IConsumeKafkaMessage<TKey, TValue, TPayload, TSession> where TSession : IDisposable;

public interface IConsumeKafkaMessage<TKey, TValue, TPayload, TSession> :
  IGetActivitySource,
  IHandleInboxMessage<TKey, TValue, TPayload, TSession>,
  IInsertInboxMessage<TKey, TValue, TPayload>,
  IPublishKafkaDeadLetter<TKey, TValue, TPayload> where TSession : IDisposable;

public interface IHandleInboxMessage<TKey, TValue, TPayload, TSession> :
  IGetStoreSession<TSession>,
  IHandleInboxMessage<TKey, TPayload>,
  IStoreSessionModel<TSession>,
  ITransactInbox<TSession>,
  IUpdateSessionInboxMessageStatus<TSession, TKey, TPayload> where TSession : IDisposable;

public interface IInsertInboxMessage<TKey, TValue, TPayload> :
  IGetUtcDateService,
  IInsertInboxMessage<TKey, TPayload>,
  IPersistedMessageMapper<TValue, TPayload>;

public interface IPublishKafkaDeadLetter<TKey, TValue, TPayload> :
  IGetLogger,
  IGetDeadLetterTopicService<TKey, TPayload>,
  IGetUtcDateService,
  IGetMetricCounters,
  IKafkaMessageMapperService<TPayload, TValue>,
  IUpdateInboxMessageStatus<TKey, TPayload>;

