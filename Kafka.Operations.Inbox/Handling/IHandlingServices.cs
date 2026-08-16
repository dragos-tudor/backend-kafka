
namespace Kafka.Operations.Inbox;

public interface IHandlingServices<TKey, TValue, TPayload, TSession> :
  IInboxMessageHandler<TKey, TPayload>,
  IInstrumentationServices,
  ISessionServices<TSession>,
  IUpdateInboxMessageSessionService<TKey, TPayload, TSession> where TSession : IDisposable;