
namespace Kafka.Operations.Inbox;

public interface IInsertingData<TKey, TValue, TPayload>:
  IInboxMessageProp<TKey, TPayload>,
  IKafkaMessageProp<TKey, TValue>,
  ITopicPartitionOffsetProp;