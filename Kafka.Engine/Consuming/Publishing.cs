
namespace Kafka.Engine;

partial class EngineFuncs
{
  static async Task<Message<TKey, TValue>> PublishKafkaDeadLetterAsync<TKey, TValue, TPayload>(
    IProducer<TKey, TValue> producer,
    InboxMessage<TKey, TPayload> message,
    string error,
    IPublishKafkaDeadLetterServices<TKey, TValue, TPayload> services,
    CancellationToken cancellationToken)
  {
    var deadLetterTopic = services.GetDeadLetterTopic(message);
    var deadLetter = ToKafkaDeadLetter(message, error, services.GetUtcDate(), services.ToKafkaMessageValue);

    await services.UpdateInboxMessageStatusAsync(message, InboxMessageStatus.DeadLettering, cancellationToken);
    await PublishMessageAsync(producer, deadLetterTopic, deadLetter, cancellationToken);

    await services.UpdateInboxMessageStatusAsync(message, InboxMessageStatus.DeadLettered, cancellationToken);
    return deadLetter;
  }
}