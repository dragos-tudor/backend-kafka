
namespace Kafka.Engine;

public interface IResumeInboxMessagesServices<TKey, TValue, TPayload, TSession> :
  IGetLoggerService where TSession : IDisposable;