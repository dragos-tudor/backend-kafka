
namespace Kafka.Operations.Outbox;

public interface IProducingData<TKey, TValue, TPayload>:
  IKafkaMessageProp<TKey, TValue>,
  IOutboxMessageProp<TKey, TPayload>,
  IProduceErrorProp;