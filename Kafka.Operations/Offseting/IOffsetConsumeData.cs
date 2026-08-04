
namespace Kafka.Operations;

public interface IOffsetConsumerData<TKey, TPayload>:
  IInboxMessageProp<TKey, TPayload>,
  IOffsetAppliedProp,
  ITopicPartitionOffsetProp;