
namespace Kafka.Pipelines;

public interface IConsumingServices<TKey, TValue, TPayload, TSession> :
  ICapturingServices<TKey, TValue>,
  IHandlingServices<TKey, TValue, TPayload, TSession>,
  IInsertingServices<TKey, TValue, TPayload>,
  IOffsettingServices<TKey, TValue>,
  Operations.Inbox.IDispatchingServices<TKey, TValue, TPayload>
  where TSession : IDisposable;

