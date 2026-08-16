
namespace Kafka.Pipelines;

public interface IResumingServices<TKey, TValue, TPayload, TSession> :
  IReadInboxMessagesService<TKey, TPayload>,
  IHandlingServices<TKey, TValue, TPayload, TSession>,
  IConvertingServices<TKey, TValue, TPayload>,
  Operations.DeadLetter.IInsertingServices<TKey, TPayload>,
  Operations.DeadLetter.IMappingServices<TKey, TValue, TPayload>,
  IProducingServices<TKey, TValue>,
  Operations.DeadLetter.ISchedulingServices<TKey, TPayload>,
  IResumeBatchSizeService
  where TSession : IDisposable;