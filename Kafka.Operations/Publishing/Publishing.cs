using static Kafka.Operations.OperationState;

namespace Kafka.Operations;

partial class OperationsFuncs
{
  static async Task<Message<TKey, TValue>> PublishDeadLetterAsync<TKey, TValue, TPayload>(
    IProducer<TKey, TValue> producer,
    Message<TKey, TValue> deadLetter,
    string deadLetterTopic,
    InboxMessage<TKey, TPayload> message,
    string? domainError,
    IPublishDeadLetterServices<TKey, TValue, TPayload> services,
    CancellationToken cancellationToken)
  {
    await services.UpdateInboxMessageStatusAsync(message, InboxMessageStatus.DeadLettering, cancellationToken);

    await PublishMessageAsync(producer, deadLetterTopic, deadLetter, cancellationToken);
    InstrumentPublishDeadLetter(message.MessageId, deadLetter.Key?.ToString(), deadLetterTopic, domainError, services);

    await services.UpdateInboxMessageStatusAsync(message, InboxMessageStatus.DeadLettered, cancellationToken);
    return deadLetter;
  }

  internal static async ValueTask<(TData, OperationState)> PublishDeadLetterAsync<TServices, TData, TKey, TValue, TPayload>(
    TServices services,
    TData data,
    CancellationToken ct)
  where TServices : IPublishDeadLetterServices<TKey, TValue, TPayload>
  where TData : IPublishDeadLetterData<TKey, TValue, TPayload>
  {
    var inboxMessage = data.InboxMessage!;
    var domainError = data.DomainError!;
    var kafkaMessage = data.KafkaMessage!;
    var topicPartitionOffset = data.TopicPartitionOffset!;

    var deadLetterTopic = services.GetDeadLetterTopic(data.InboxMessage!);
    var deadLetter = ToKafkaDeadLetter(inboxMessage!, topicPartitionOffset, domainError, services.GetUtcDate(), services.ToKafkaMessageValue);

    InjectTraceParentActivity(Activity.Current, kafkaMessage.Headers);
    await PublishDeadLetterAsync(services.GetProducer(), deadLetter, deadLetterTopic, inboxMessage,  domainError, services, ct);

    return (data, PublishedDeadLetterState);
  }
}