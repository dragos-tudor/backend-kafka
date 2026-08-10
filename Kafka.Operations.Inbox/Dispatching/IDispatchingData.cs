
namespace Kafka.Operations.Inbox;

public interface IDispatchingData<TKey, TValue, TPayload>:
  IKafkaMessageProp<TKey, TValue>,
  IInboxMessageProp<TKey, TPayload>,
  IDeadLetterProp<TKey, TValue>,
  IDispatchErrorProp,
  ITopicPartitionOffsetProp,
  IHandleErrorProp;