
namespace Kafka.Operations.Inbox;

public interface IDispatchingData<TKey, TValue, TPayload>:
  IDeadLetterProp<TKey, TValue>,
  IDispatchErrorProp,
  IInboxMessageProp<TKey, TPayload>,
  IKafkaMessageProp<TKey, TValue>,
  IPipelineProp,
  ITopicPartitionOffsetProp,
  IHandleErrorProp;