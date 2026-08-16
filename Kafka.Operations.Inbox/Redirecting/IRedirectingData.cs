
namespace Kafka.Operations.Inbox;

public interface IRedirectingData<TKey, TValue, TPayload>:
  IInboxMessageErrorProp,
  IKafkaMessageProp<TKey, TValue>,
  ITopicPartitionOffsetProp;