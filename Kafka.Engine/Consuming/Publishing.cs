
namespace Kafka.Engine;

partial class EngineFuncs
{
  static async Task<Message<TKey, TValue>> PublishKafkaDeadLetterAsync<TKey, TValue, TPayload>(
    IProducer<TKey, TValue> producer,
    InboxMessage<TKey, TPayload> message,
    TopicPartitionOffset offset,
    string failureReason,
    Activity? activity,
    IPublishKafkaDeadLetter<TKey, TValue, TPayload> services,
    CancellationToken cancellationToken)
  {
    var deadLetterTopic = services.GetDeadLetterTopic(message);
    var deadLetter = ToKafkaDeadLetter(message, offset, failureReason, services.GetUtcDate(), services.ToKafkaMessageValue);

    await services.UpdateInboxMessageStatusAsync(message, InboxMessageStatus.DeadLettering, cancellationToken);

    if (activity is not null)
      InjectMessageActivityContext(activity, deadLetter.Headers);
    await PublishMessageAsync(producer, deadLetterTopic, deadLetter, cancellationToken);
    LogPublishedDeadLetter(services.GetLogger(), message.MessageId, offset, message.CorrelationId);

    var metricCounters = services.GetMetricCounters();
    metricCounters[MetricCounterTypes.DeadLettered].Add(1);
    AddActivityTag(activity, "deadletter.topic", deadLetterTopic);
    AddActivityTag(activity, "deadletter.reason", failureReason);
    AddActivityEvent(activity, "deadletter.published");

    await services.UpdateInboxMessageStatusAsync(message, InboxMessageStatus.DeadLettered, cancellationToken);
    return deadLetter;
  }
}