using static Kafka.Engine.ConsumingState;

namespace Kafka.Engine;

partial class EngineFuncs
{
  // consuming kafka message feature coordinator (state machine).
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

    using var activity = CreateMessageActivity(services.GetActivitySource(), "consume-kafka-message", result, messageId, correlationId);
    using var logScope = CreateLogScopeForMessage(services.GetLogger(), activity, "consume-kafka-message", offset, messageId, correlationId);
    var counters = services.GetMetricCounters();

    LogCapturedKafkaMessage(services.GetLogger());
    IncrementMetricCounter(counters, MetricCounterTypes.Captured);
    yield return CapturedKafkaMessageState;

    AddActivityTag(activity, "message.id", messageId);
    AddActivityTag(activity, "message.topic", result.TopicPartitionOffset.Topic);
    AddActivityTag(activity, "message.partition", result.TopicPartitionOffset.Partition);
    AddActivityEvent(activity, "message.captured");

    yield return InsertingInboxMessageState;
    var message = await InsertInboxMessageAsync(result, services, cancellationToken);
    if (message is not null)
    {
      LogInsertedInboxMessage(services.GetLogger());
      AddActivityEvent(activity, "inbox.inserted");
      yield return InsertedInboxMessageState;
    }

    yield return ApplyingConsumerOffsetState;
    var appliedOffset = ApplyConsumerOffsetStrategy(consumer, offset, kafkaOptions);
    LogAppliedConsumerOffset(services.GetLogger());
    AddActivityTag(activity, "offset.applied", appliedOffset);
    yield return AppliedConsumerOffsetState;

    if (message is null) { yield return AlreadySavedInboxMessageState; yield break; }

    yield return HandlingInboxMessageState;
    var domainError = await HandleInboxMessageAsync(message, services, cancellationToken);
    IncrementMetricCounter(counters, MetricCounterTypes.Handled);
    if (domainError is not null)
    {
      LogHandledInboxMessageFailed(services.GetLogger(), domainError);
      AddActivityTag(activity, "domain.error", domainError);
      AddActivityEvent(activity, "message.handling.failed", [new KeyValuePair<string, object?>("error", domainError)]);

      yield return PublishingDeadLetterState;
      using var publishScope = CreateLogScopeForActivity(services.GetLogger(), activity, "publish-dead-letter");
      await PublishKafkaDeadLetterAsync(producer, message, offset, domainError, activity, services, cancellationToken);
      yield return PublishedDeadLetterState;
      yield break;
    }

    LogHandledInboxMessage(services.GetLogger());
    AddActivityEvent(activity, "message.handled");
    yield return HandledInboxMessageState;
  }
}