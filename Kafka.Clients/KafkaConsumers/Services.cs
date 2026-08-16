
namespace Kafka.Clients;

public interface IConsumerService<TKey, TValue> { IConsumer<TKey, TValue> GetConsumer(); }
