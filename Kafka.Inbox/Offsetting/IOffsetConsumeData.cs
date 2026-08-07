
namespace Kafka.Inbox;

public interface IOffsetConsumerData<TKey, TPayload>:
  IInboxMessageProp<TKey, TPayload>,
  ITopicPartitionOffsetProp;