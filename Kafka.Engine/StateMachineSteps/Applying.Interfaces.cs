
namespace Kafka.Engine;

public interface IApplyConsumerOffsetServices<TKey, TValue> :
  IInstrumentationServices,
  IGetConsumerService<TKey, TValue>,
  IGetKafkaOptionsService;

public interface IApplyConsumerOffsetData:
  IAppliedOffsetData,
  ITopicPartitionOffsetData;