
namespace Kafka.Operations;

public interface IInsertInboxMessageData<TKey, TValue, TPayload>:
  IInboxMessageProp<TKey, TPayload>,
  IKafkaMessageProp<TKey, TValue>,
  ITopicPartitionOffsetProp;