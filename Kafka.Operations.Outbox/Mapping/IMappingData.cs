
namespace Kafka.Operations.Outbox;

public interface IMappingData<TKey, TValue, TPayload>:
  IOutboxMessageProp<TKey, TPayload>,
  IKafkaMessageProp<TKey, TValue>;