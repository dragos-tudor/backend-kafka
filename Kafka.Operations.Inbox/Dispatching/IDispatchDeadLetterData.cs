
namespace Kafka.Operations.Inbox;

public interface IDispatchDeadLetterData<TKey, TValue, TPayload>:
  IKafkaMessageProp<TKey, TValue>,
  IInboxMessageProp<TKey, TPayload>,
  IDeadLetterProp<TKey, TValue>,
  ITopicPartitionOffsetProp,
  IHandleErrorProp;