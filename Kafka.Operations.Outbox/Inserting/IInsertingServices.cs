
namespace Kafka.Operations.Outbox;

 public interface IInsertingServices<TKey, TPayload, TSession> :
  IInsertOutboxMessageSessionService<TKey, TPayload, TSession>,
  IInstrumentationServices,
  ISessionServices<TSession> where TSession : IDisposable;