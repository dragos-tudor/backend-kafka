
namespace Kafka.Pipelines;

public interface IRelayingServices<TKey, TValue, TPayload> :
  IReadOutboxMessagesService<TKey, TPayload>,
  Operations.Outbox.IMappingServices<TKey, TValue, TPayload>,
  IProducingServices<TKey, TValue, TPayload>,
  Operations.Outbox.ISchedulingServices<TKey, TPayload>,
  IRelayBatchSizeService;