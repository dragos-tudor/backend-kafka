
namespace Kafka.Pipelines;

public interface IRedeliveringServices<TKey, TValue, TPayload> :
  IReadDeadLetterMessagesService<TKey, TPayload>,
  Operations.DeadLetter.IMappingServices<TKey, TValue, TPayload>,
  Operations.DeadLetter.IProducingServices<TKey, TValue>,
  Operations.DeadLetter.ISchedulingServices<TKey, TPayload>,
  IRelayBatchSizeService;