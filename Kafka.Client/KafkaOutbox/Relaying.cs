
namespace Kafka.Client;

partial class KafkaFuncs
{
  // Relays ONE due outbox item to Kafka: on repeated publish failure, either signals a retry
  // (with the next backoff delay) or dead-letters the message (no original TopicPartitionOffset exists yet).
  public static async Task<KafkaMessageProcessingResult> RelayOutboxMessageAsync<TItem, TKey>(
    IProducer<TKey, byte[]> producer,
    IProducer<TKey, byte[]> deadLetterProducer,
    KafkaOptions options,
    TItem outboxItem,
    Func<TItem, Message<TKey, byte[]>> mapToKafkaMessage,
    Func<TItem, string> getTopic,
    Func<TItem, int> getPublishAttemptCount,
    CancellationToken cancellationToken = default)
  {
    var topic = getTopic(outboxItem);
    var message = mapToKafkaMessage(outboxItem);

    try
    {
      await PublishMessageAsync(producer, topic, message, cancellationToken);
      return new(KafkaMessageOutcome.Succeeded);
    }
    catch (ProduceException<TKey, byte[]> exception)
    {
      var attemptCount = getPublishAttemptCount(outboxItem) + 1;
      if (attemptCount < options.MaxRetryAttempts)
        return new(KafkaMessageOutcome.Retrying, exception.Error.Reason, CalculateNextRetryDelay(attemptCount, options));

      var deadLetter = CreateKafkaDeadLetter(exception.Error.Reason, message);
      await PublishMessageAsync(
        deadLetterProducer,
        GetDeadLetterTopicName(topic, options.DeadLetterTopicSuffix),
        deadLetter,
        cancellationToken);
      return new(KafkaMessageOutcome.DeadLettered, exception.Error.Reason);
    }
  }
}
