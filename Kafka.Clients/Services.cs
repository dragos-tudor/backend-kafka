
namespace Kafka.Clients;

public interface IConsumerService<TKey, TValue> { IConsumer<TKey, TValue> GetConsumer(); }

public interface IProducerService<TKey, TValue> { IProducer<TKey, TValue> GetProducer(); }

public interface IKafkaOptionsService { KafkaOptions GetKafkaOptions(); }
