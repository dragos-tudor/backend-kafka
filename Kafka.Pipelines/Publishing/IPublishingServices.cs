
namespace Kafka.Pipelines;

public interface IPublishingServices<TKey, TValue, TPayload, TSession> :
  Operations.Outbox.IValidatingServices<TKey, TPayload>,
  Operations.Outbox.IInsertingServices<TKey, TPayload, TSession>,
  Operations.Outbox.IMappingServices<TKey, TValue, TPayload>,
  Operations.Outbox.IProducingServices<TKey, TValue, TPayload>,
  Operations.Outbox.ISchedulingServices<TKey, TPayload>,
  IRelayBatchSizeService
  where TSession: IDisposable;