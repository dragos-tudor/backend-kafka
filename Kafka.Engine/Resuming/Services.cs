
namespace Kafka.Engine;

public interface IResumeInboxMessagesServices<TKey, TValue, TPayload, TSession> :
  IGetLogger where TSession : IDisposable;