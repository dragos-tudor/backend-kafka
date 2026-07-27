
namespace Kafka;

partial class KafkaFuncs
{
  internal static async Task<ConsumeKafkaMessageError?> ConsumeKafkaMessageAsync<TKey, TValue, TPayload>(
    IConsumer<TKey, TValue> consumer,
    IProducer<TKey, TValue> producer,
    KafkaOptions kafkaOptions,
    IConsumeKafkaMessageServices<TKey, TValue, TPayload> services,
    CancellationToken cancellationToken = default)
  {
    var logger = services.GetLogger("ConsumeKafkaMessage");

    var (result, getFailure) = GetConsumerKafkaMessage(consumer, logger, cancellationToken);
    if (getFailure is not null) return ToConsumeMessageError(getFailure.Value);

    var offset = result!.TopicPartitionOffset;

    var (inboxMessage, saveFailure) = await SaveInboxMessageAsync(result!.Message, offset, services, logger, cancellationToken);
    if (saveFailure is not null && saveFailure != SaveInboxMessageError.InboxMessageAlreadySaved) return ToConsumeMessageError(saveFailure.Value);

    var offsetFailure = ApplyConsumerOffset(consumer, offset, inboxMessage?.MessageId, kafkaOptions, logger);
    if (offsetFailure is not null) return ToConsumeMessageError(offsetFailure.Value);
    if (saveFailure == SaveInboxMessageError.InboxMessageAlreadySaved) return ToConsumeMessageError(saveFailure.Value);

    var handleFailure = await HandleInboxMessageAsync(producer, inboxMessage!, offset, services, logger, cancellationToken);
    if (handleFailure is not null) return ToConsumeMessageError(handleFailure.Value);

    return default;
  }
}