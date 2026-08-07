
namespace Kafka.Inbox;

public interface IOffsetConsumerServices<TKey, TValue> :
  IConsumerService<TKey, TValue>,
  IInstrumentationServices,
  IKafkaOptionsService;