namespace Kafka.Clients;

partial class ClientsFuncs
{
  internal static TopicPartitionOffset? ApplyConsumerOffsetStrategy<TKey, TValue>(
    IConsumer<TKey, TValue> consumer,
    TopicPartitionOffset offset,
    bool enableAutoOffsetStore,
    bool enableAutoCommit)
  =>
    (enableAutoOffsetStore, enableAutoCommit) switch {
      (true, true) => default,
      (false, true) => StoreConsumerOffset(consumer, offset),
      (_, false) => CommitConsumerOffset(consumer, offset),
    };
}