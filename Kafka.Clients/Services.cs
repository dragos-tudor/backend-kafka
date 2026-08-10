
namespace Kafka.Clients;

public interface IConsumerService<TKey, TValue> { IConsumer<TKey, TValue> GetConsumer(string key, bool renew = false); }

public interface IProducerService<TKey, TValue> { IProducer<TKey, TValue> GetProducer(string key, bool renew = false); }

public interface IKafkaOptionsService { KafkaOptions GetKafkaOptions(); }
