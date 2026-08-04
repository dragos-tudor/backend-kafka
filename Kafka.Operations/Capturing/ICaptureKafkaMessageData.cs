
namespace Kafka.Operations;

public interface ICaptureKafkaMessageData<TKey, TValue, TPayload>:
  IKafkaMessageProp<TKey, TValue>,
  IInboxMessageProp<TKey, TPayload>,
  ITopicPartitionOffsetProp;