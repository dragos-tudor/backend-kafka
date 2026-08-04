
namespace Kafka.Operations;

public interface IPublishDeadLetterData<TKey, TValue, TPayload>:
  IKafkaMessageProp<TKey, TValue>,
  IInboxMessageProp<TKey, TPayload>,
  ITopicPartitionOffsetProp,
  IDomainErrorProp;