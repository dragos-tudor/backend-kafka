
namespace Kafka.Client;

partial class KafkaFuncs
{
  // Resumes processing of ONE due inbox item: re-invokes the handler, and on repeated
  // failure either signals a retry (with the next backoff delay) or dead-letters the message.
  public static async Task<KafkaMessageProcessingResult> ResumeInboxMessageAsync<TItem, TKey>(
    IProducer<TKey, byte[]> deadLetterProducer,
    KafkaOptions options,
    TItem inboxItem,
    Func<TItem, Message<TKey, byte[]>> mapToKafkaMessage,
    Func<TItem, TopicPartitionOffset> getTopicPartitionOffset,
    Func<TItem, int> getRetryCount,
    HandleConsumerMessage<TKey> handleMessage,
    CancellationToken cancellationToken = default)
  {
    var message = mapToKafkaMessage(inboxItem);
    var failure = await handleMessage(message);
    if (failure is null) return new(KafkaMessageOutcome.Succeeded);

    var retryCount = getRetryCount(inboxItem) + 1;
    if (retryCount < options.MaxRetryAttempts)
      return new(KafkaMessageOutcome.Retrying, failure, CalculateNextRetryDelay(retryCount, options));

    var topicPartitionOffset = getTopicPartitionOffset(inboxItem);
    var deadLetter = CreateKafkaDeadLetter(failure, message, topicPartitionOffset);
    await PublishMessageAsync(
      deadLetterProducer,
      GetDeadLetterTopicName(topicPartitionOffset.Topic, options.DeadLetterTopicSuffix),
      deadLetter,
      cancellationToken);
    return new(KafkaMessageOutcome.DeadLettered, failure);
  }
}
