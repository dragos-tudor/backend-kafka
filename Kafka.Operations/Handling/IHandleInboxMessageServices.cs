
namespace Kafka.Operations;

public interface IHandleInboxMessageServices<TKey, TValue, TPayload, TSession> :
  IStoreSessionService<TSession>,
  IInstrumentationServices,
  IHandleInboxMessageService<TKey, TPayload>,
  IStoreSessionModelService<TSession>,
  ITransactSessionService<TSession>,
  IUpdateInboxMessageStatusService<TKey, TPayload>,
  IUpdateSessionInboxMessageStatusService<TKey, TPayload, TSession> where TSession : IDisposable;