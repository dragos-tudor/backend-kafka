
namespace Kafka.Clients;

partial class ClientsFuncs
{
  internal static TopicPartitionOffset? OffsetConsumer<TKey, TValue>(
    IConsumer<TKey, TValue> consumer,
    TopicPartitionOffset offset,
    KafkaOptions kafkaOptions)
  =>
    (kafkaOptions.EnableAutoOffsetStore, kafkaOptions.EnableAutoCommit) switch {
      (true, true) => default,
      (false, true) => StoreConsumerOffset(consumer, offset),
      (_, false) => CommitConsumerOffset(consumer, offset),
    };
}