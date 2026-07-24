namespace Kafka.Client;

partial class KafkaFuncs
{
  public static ConsumeResult<TKey, byte[]> ConsumeMessage<TKey>(
    IConsumer<TKey, byte[]> consumer,
    CancellationToken cancellationToken = default)
  => consumer.Consume(cancellationToken);

  // Capture-only loop: persists an inbox record for every consumed message and immediately
  // advances the offset (per the chosen commit strategy). Actual handling/retries happen later,
  // asynchronously, via ResumeInboxMessageAsync/ProcessDueMessagesAsync.
  public static async Task ConsumeMessagesAsync<TKey>(
    IConsumer<TKey, byte[]> consumer,
    KafkaCommitStrategy commitStrategy,
    Func<Message<TKey, byte[]>, TopicPartitionOffset, CancellationToken, Task> saveInboxMessage,
    CancellationToken cancellationToken = default)
  {
    while (!cancellationToken.IsCancellationRequested)
    {
      var result = ConsumeMessage(consumer, cancellationToken);
      if (result is null || result.IsPartitionEOF) continue;

      await saveInboxMessage(result.Message, result.TopicPartitionOffset, cancellationToken);
      ApplyCommitStrategy(consumer, result, commitStrategy);
    }
  }
}