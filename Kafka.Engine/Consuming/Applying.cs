namespace Kafka.Engine;

partial class EngineFuncs
{
  internal static TopicPartitionOffset? ApplyConsumerOffsetStrategy<TKey, TValue>(
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