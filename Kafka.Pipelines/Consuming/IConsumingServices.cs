
namespace Kafka.Pipelines;

public interface IConsumingServices<TKey, TValue, TPayload, TSession> :
  ICapturingServices<TKey, TValue>,
  IRedirectingServices<TKey, TValue, TPayload>,
  Operations.Inbox.IMappingServices<TKey, TValue, TPayload>,
  Operations.Inbox.IValidatingServices<TKey, TPayload>,
  Operations.Inbox.IInsertingServices<TKey, TPayload>,
  IOffsettingServices<TKey, TValue>,
  IHandlingServices<TKey, TPayload, TSession>,
  Operations.Inbox.ISchedulingServices<TKey, TPayload>
  where TSession : IDisposable;

