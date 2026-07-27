
using static Kafka.HandleInboxMessageError;

namespace Kafka;

partial class KafkaFuncs
{
  internal static async Task<HandleInboxMessageError?> HandleInboxMessageAsync<TKey, TValue, TPayload>(
    IProducer<TKey, TValue> producer,
    InboxMessage<TKey, TPayload> inboxMessage,
    TopicPartitionOffset offset,
    IHandleInboxMessageServices<TKey, TValue, TPayload> services,
    ILogger logger,
    CancellationToken cancellationToken)
  {
    try
    {
      var failure = await services.HandleInboxMessage(inboxMessage, cancellationToken);
      if (failure is not null)
      {
        var deadLetterMessage = ToKafkaDeadLetter(inboxMessage, failure, services.GetUtcDate(), services.ToMessageValue);
        var deadLetterTopic = services.GetDeadLetterTopic(offset.Topic);

        await services.UpdateInboxMessageStatus(inboxMessage, InboxMessageStatus.DeadLettering, cancellationToken);
        await PublishMessageAsync(producer, deadLetterTopic, deadLetterMessage, cancellationToken);

        await services.UpdateInboxMessageStatus(inboxMessage, InboxMessageStatus.Failed, cancellationToken);
        return MessageDeadLettered;
      }

      await services.UpdateInboxMessageStatus(inboxMessage, InboxMessageStatus.Handled, cancellationToken);
      return default;
    }
    catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested) { return OperationCanceled; }
    catch (Exception exception)
    {
      LogHandleInboxMessageFailed(logger, exception, inboxMessage.MessageId, offset);
      return HandleInboxMessageFailed;
    }
  }
}