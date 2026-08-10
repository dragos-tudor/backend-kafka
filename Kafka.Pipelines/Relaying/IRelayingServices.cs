
namespace Kafka.Pipelines;

public interface IRelayingServices<TKey, TValue, TPayload> :
  IGetOutboxMessagesService<TKey, TPayload>,
  IPublishingServices<TKey, TValue, TPayload>,
  Operations.Outbox.ISchedulingServices<TKey, TPayload>,
  Operations.Outbox.IDispatchingServices<TKey, TValue, TPayload>,
  Operations.Outbox.IDelayingServices<TKey, TValue, TPayload>,
  IRelayBatchSizeService;