
namespace Kafka.Operations.Inbox;

public interface IHandlingServices<TKey, TValue, TPayload, TSession> :
  IStoreSessionService<TSession>,
  IInstrumentationServices,
  IHandleInboxMessageService<TKey, TPayload>,
  IStoreModelSessionService<TSession>,
  ITransactSessionService<TSession>,
  IUpdateIntegrationMessageService<TKey, TPayload>,
  IUpdateIntegrationMessageSessionService<TKey, TPayload, TSession> where TSession : IDisposable;