
namespace Kafka.Operations;

public interface IOffsetConsumerServices<TKey, TValue> :
  IConsumerService<TKey, TValue>,
  IInstrumentationServices,
  IKafkaOptionsService;