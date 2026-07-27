
using static Kafka.ApplyConsumerOffsetError;

namespace Kafka;

partial class KafkaFuncs
{
  internal static ApplyConsumerOffsetError? ApplyConsumerOffset<TKey, TValue>(
    IConsumer<TKey, TValue> consumer,
    TopicPartitionOffset offset,
    Guid? messageId,
    KafkaOptions kafkaOptions,
    ILogger logger)
  {
    try
    {
      var autoOffsetStore = kafkaOptions.EnableAutoOffsetStore;
      var autoCommit = kafkaOptions.EnableAutoCommit;
      ApplyConsumerOffsetStrategy(consumer, offset, autoOffsetStore, autoCommit);
      return default;
    }
    catch (Exception exception)
    {
      LogApplyConsumerOffsetFailed(logger, exception, messageId, offset);
      return ApplyConsumerOffsetFailed;
    }
  }
}