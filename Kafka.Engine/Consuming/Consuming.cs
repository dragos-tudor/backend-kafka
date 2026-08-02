using static Kafka.Engine.ConsumingState;

namespace Kafka.Engine;

partial class EngineFuncs
{
  // consuming Kafka message feature coordinator (state machine).
  internal static async IAsyncEnumerable<ConsumingState> ConsumeKafkaMessageAsync<TKey, TValue, TPayload, TSession>(
    IConsumer<TKey, TValue> consumer,
    IProducer<TKey, TValue> producer,
    KafkaOptions kafkaOptions,
    IConsumeKafkaMessage<TKey, TValue, TPayload, TSession> services,
    [EnumeratorCancellation] CancellationToken cancellationToken = default)
  where TSession: IDisposable
  {
    var result = CaptureKafkaMessage(consumer, cancellationToken);
    if (result is null) { yield return NotConsumedMessageState; yield break; }

    var messageId = GetMessageIdKafkaHeader(result.Message.Headers);
    var correlationId = GetCorrelationIdKafkaHeader(result.Message.Headers);
    var offset = result.TopicPartitionOffset;

    using var activity = CreateMessageActivity(services.GetActivitySource(), "consume-kafka-message", result, messageId, correlationId, KafkaConsumer);
    using var logScope = CreateLogScopeForMessage(services.GetLogger(), activity, offset, messageId, correlationId);
    var counters = services.GetMetricCounters();

    AddActivityTag(activity, "message.id", messageId);
    AddActivityTag(activity, "message.topic", result.TopicPartitionOffset.Topic);
    AddActivityTag(activity, "message.partition", result.TopicPartitionOffset.Partition);
    AddActivityEvent(activity, "message.captured");

    LogCapturedKafkaMessage(services.GetLogger(), messageId, offset, correlationId);
    yield return CapturedKafkaMessageState;

    yield return InsertingInboxMessageState;
    var message = await InsertInboxMessageAsync(result, services, cancellationToken);
    if (message is not null)
    {
      AddActivityEvent(activity, "inbox.inserted");
      LogInsertedInboxMessage(services.GetLogger(), messageId, offset, correlationId);
      yield return InsertedInboxMessageState;
    }

    yield return ApplyingConsumerOffsetState;
    var appliedOffset = ApplyConsumerOffsetStrategy(consumer, offset, kafkaOptions);
    AddActivityTag(activity, "offset.applied", appliedOffset);
    LogAppliedConsumerOffset(services.GetLogger(), messageId, appliedOffset, correlationId);
    yield return AppliedConsumerOffsetState;

    if (message is null) { yield return AlreadySavedInboxMessageState; yield break; }

    yield return HandlingInboxMessageState;
    var domainError = await HandleInboxMessageAsync(message, services, cancellationToken);
    LogHandledInboxMessage(services.GetLogger(), messageId, offset, domainError, correlationId);

    if (domainError is not null)
    {
      counters[MetricCounterTypes.ConsumingErrors].Add(1);
      AddActivityTag(activity, "domain.error", domainError);
      AddActivityEvent(activity, "message.handling.failed", [new KeyValuePair<string, object?>("error", domainError)]);

      yield return PublishingDeadLetterState;
      using var publishScope = CreateLogScopeForActivity(services.GetLogger(), activity, "publish-dead-letter");
      await PublishKafkaDeadLetterAsync(producer, message, offset, domainError, activity, services, cancellationToken);
      yield return PublishedDeadLetterState;
      yield break;
    }

    counters[MetricCounterTypes.Handled].Add(1);
    AddActivityEvent(activity, "message.handled");
    yield return HandledInboxMessageState;
  }
}