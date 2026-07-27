
namespace Kafka.Clients;

partial class ClientsFuncs
{
  static TopicPartitionOffset StoreConsumerOffset<TKey, TValue>(
    IConsumer<TKey, TValue> consumer,
    TopicPartitionOffset offset)
  {
    consumer.StoreOffset(offset);
    return offset;
  }
}
