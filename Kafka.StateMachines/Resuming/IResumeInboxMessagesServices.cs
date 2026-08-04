
namespace Kafka.StateMachines;

public interface IResumeInboxMessagesServices<TKey, TValue, TPayload, TSession> :
  ILoggerService where TSession : IDisposable;