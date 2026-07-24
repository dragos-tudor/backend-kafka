namespace Kafka.Client;

partial class KafkaFuncs
{
  public static void CommitConsumedMessage<TKey, TValue>(
    IConsumer<TKey, TValue> consumer,
    ConsumeResult<TKey, TValue> consumeResult)
    => consumer.Commit(consumeResult);

  public static void ApplyCommitStrategy<TKey, TValue>(
    IConsumer<TKey, TValue> consumer,
    ConsumeResult<TKey, TValue> consumeResult,
    KafkaCommitStrategy commitStrategy)
  {
    switch (commitStrategy)
    {
      case KafkaCommitStrategy.StoreOffset:
        StoreConsumedMessageOffset(consumer, consumeResult);
        break;
      case KafkaCommitStrategy.DirectCommit:
        CommitConsumedMessage(consumer, consumeResult);
        break;
    }
  }
}