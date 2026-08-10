
namespace Kafka.Operations.Inbox;

public interface IOffsettingServices<TKey, TValue> :
  IConsumerService<TKey, TValue>,
  IInstrumentationServices,
  IKafkaOptionsService;