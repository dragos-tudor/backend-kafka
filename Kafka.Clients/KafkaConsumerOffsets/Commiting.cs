namespace Kafka.Clients;

partial class ClientsFuncs
{
  internal static TopicPartitionOffset CommitConsumerOffset<TKey, TValue>(
    IConsumer<TKey, TValue> consumer,
    TopicPartitionOffset offset)
  {
    consumer.Commit([offset]);
    return offset;
  }
}