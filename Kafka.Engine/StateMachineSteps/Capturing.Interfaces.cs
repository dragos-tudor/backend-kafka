
namespace Kafka.Engine;

public interface ICaptureKafkaMessageServices<TKey, TValue> :
  IInstrumentationServices,
  IGetConsumerService<TKey, TValue>;

public interface ICaptureKafkaMessageData<TKey, TValue, TPayload>:
  IKafkaMessageData<TKey, TValue>,
  IInboxMessageData<TKey, TPayload>,
  ICorrelationIdData,
  IMessageIdData,
  ITopicPartitionOffsetData;