
namespace Kafka.Engine;

partial class EngineFuncs
{
  static async Task<Message<TKey, TValue>> PublishKafkaDeadLetterAsync<TKey, TValue, TPayload>(
    IProducer<TKey, TValue> producer,
    Message<TKey, TValue> deadLetter,
    string deadLetterTopic,
    InboxMessage<TKey, TPayload> message,
    IPublishDeadLetterServices<TKey, TValue, TPayload> services,
    CancellationToken cancellationToken)
  {
    await services.UpdateInboxMessageStatusAsync(message, InboxMessageStatus.DeadLettering, cancellationToken);

    await PublishMessageAsync(producer, deadLetterTopic, deadLetter, cancellationToken);

    await services.UpdateInboxMessageStatusAsync(message, InboxMessageStatus.DeadLettered, cancellationToken);
    return deadLetter;
  }
}