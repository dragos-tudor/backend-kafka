
namespace Kafka.Operations.Inbox;

public interface IHandlingServices<TKey, TPayload, TSession> :
  IInboxMessageHandler<TKey, TPayload>,
  IInstrumentationServices,
  ISessionServices<TSession>,
  IUpdateInboxMessageService<TKey, TPayload>,
  IUpdateInboxMessageSessionService<TKey, TPayload, TSession>
  where TSession : IDisposable;