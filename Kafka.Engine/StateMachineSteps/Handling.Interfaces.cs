
namespace Kafka.Engine;

public interface IHandleInboxMessageServices<TKey, TValue, TPayload, TSession> :
  IGetStoreSessionService<TSession>,
  IInstrumentationServices,
  IHandleInboxMessageService<TKey, TPayload>,
  IStoreSessionModelService<TSession>,
  ITransactSessionService<TSession>,
  IUpdateInboxMessageStatusService<TKey, TPayload>,
  IUpdateSessionInboxMessageStatusService<TKey, TPayload, TSession> where TSession : IDisposable;

public interface IHandleInboxMessageData<TKey, TValue, TPayload>:
  IInboxMessageData<TKey, TPayload>,
  IDomainErrorData;