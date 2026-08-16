
namespace Kafka.Clients;

public interface IProducerService<TKey, TValue> { IProducer<TKey, TValue> GetProducer(); }
