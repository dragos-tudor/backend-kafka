using static Kafka.Engine.ConsumingState;

namespace Kafka.Engine;

partial class EngineFuncs
{
  internal static async IAsyncEnumerable<ConsumingState> ConsumeKafkaMessageAsync<TKey, TValue, TPayload, TSession>(
    IConsumer<TKey, TValue> consumer,
    IProducer<TKey, TValue> producer,
    KafkaOptions kafkaOptions,
    IConsumeKafkaMessageServices<TKey, TValue, TPayload, TSession> services,
    [EnumeratorCancellation] CancellationToken cancellationToken = default)
  where TSession: IDisposable
  {
    var result = CaptureKafkaMessage(consumer, cancellationToken);
    if (result is null) { yield return NotConsumedMessageState; yield break; }

    yield return CapturedKafkaMessageState;
    var messageId = GetMessageIdKafkaHeader(result.Message.Headers);
    var offset = result.TopicPartitionOffset;
    LogCapturedKafkaMessage(services.GetLogger(), messageId, offset);

    yield return InsertingInboxMessageState;
    var message = await InsertInboxMessageAsync(result, services, cancellationToken);
    if (message is not null) {
      LogInsertedInboxMessage(services.GetLogger(), messageId, offset);
      yield return InsertedInboxMessageState;
    }

    yield return ApplyingConsumerOffsetState;
    var appliedOffset = ApplyConsumerOffsetStrategy(consumer, offset, kafkaOptions);
    LogAppliedConsumerOffset(services.GetLogger(), messageId, appliedOffset);
    yield return AppliedConsumerOffsetState;

    if (message is null) { yield return AlreadySavedInboxMessageState; yield break; }

    yield return HandlingInboxMessageState;
    var handleError = await HandleInboxMessageAsync(message, services, cancellationToken);
    LogHandledInboxMessage(services.GetLogger(), messageId, offset, handleError);
    if (handleError is null) { yield return HandledInboxMessageState; yield break; }

    yield return PublishingDeadLetterState;
    await PublishKafkaDeadLetterAsync(producer, message, handleError, services, cancellationToken);
    LogPublishedDeadLetter(services.GetLogger(), messageId, offset);
    yield return PublishedDeadLetterState;
  }
}