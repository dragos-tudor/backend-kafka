
namespace Kafka.Operations.Inbox;

public interface IMappingData<TKey, TValue, TPayload>:
  IInboxMessageProp<TKey, TPayload>,
  IKafkaMessageProp<TKey, TValue>,
  ITopicPartitionOffsetProp,
  IInboxMessageErrorProp;