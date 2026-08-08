
namespace Kafka.Operations.Inbox;

public interface IOffsetConsumerData<TKey, TPayload>:
  IInboxMessageProp<TKey, TPayload>,
  ITopicPartitionOffsetProp;