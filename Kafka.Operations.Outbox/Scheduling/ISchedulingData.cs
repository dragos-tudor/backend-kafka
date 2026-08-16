
namespace Kafka.Operations.Outbox;

public interface ISchedulingData<TKey, TPayload>:
  IOutboxMessageProp<TKey, TPayload>,
  IProduceErrorProp;