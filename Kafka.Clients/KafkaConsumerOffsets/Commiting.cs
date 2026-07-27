namespace Kafka.Clients;

partial class ClientsFuncs
{
  static TopicPartitionOffset CommitConsumerOffset<TKey, TValue>(
    IConsumer<TKey, TValue> consumer,
    TopicPartitionOffset offset)
  {
    consumer.Commit([offset]);
    return offset;
  }
}