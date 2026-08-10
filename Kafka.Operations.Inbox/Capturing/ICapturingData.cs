
namespace Kafka.Operations.Inbox;

public interface ICapturingData<TKey, TValue>:
  IKafkaMessageProp<TKey, TValue>,
  IPipelineProp,
  ITopicPartitionOffsetProp;