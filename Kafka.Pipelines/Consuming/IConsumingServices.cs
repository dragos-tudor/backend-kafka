
namespace Kafka.Pipelines;

public interface IConsumingServices<TKey, TValue, TPayload, TSession> :
  ICapturingServices<TKey, TValue>,
  IRedirectingServices<TKey, TValue, TPayload>,
  Operations.Inbox.IMappingServices<TKey, TValue, TPayload>,
  Operations.Inbox.IValidatingServices<TKey, TPayload>,
  IInsertingServices<TKey, TPayload, TSession>,
  IOffsettingServices<TKey, TValue>,
  IHandlingServices<TKey, TValue, TPayload, TSession>,
  IConvertingServices<TKey, TValue, TPayload>,
  Operations.DeadLetter.IInsertingServices<TKey, TPayload>,
  Operations.DeadLetter.IMappingServices<TKey, TValue, TPayload>,
  IProducingServices<TKey, TValue>,
  Operations.DeadLetter.ISchedulingServices<TKey, TPayload>
  where TSession : IDisposable;

