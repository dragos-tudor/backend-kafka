using static Kafka.SaveInboxMessageError;

namespace Kafka;

partial class KafkaFuncs
{
  internal static async Task<Result<InboxMessage<TKey, TPayload>?, SaveInboxMessageError?>> SaveInboxMessageAsync<TKey, TValue, TPayload>(
    Message<TKey, TValue> message,
    TopicPartitionOffset offset,
    ISaveInboxMessageServices<TKey, TValue, TPayload> services,
    ILogger logger,
    CancellationToken cancellationToken)
  {
    try
    {
      var inboxMessage = ToInboxMessage(message, offset, services.ToMessagePayload);
      var inboxMessageSaved = await services.SaveInboxMessage(inboxMessage, offset, cancellationToken);

      return inboxMessageSaved
        ? inboxMessage
        : InboxMessageAlreadySaved;
    }
    catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested) { return OperationCanceled; }
    catch (Exception exception)
    {
      var messageId = GetMessageIdKafkaHeader(message.Headers);
      LogSaveInboxMessageFailed(logger, exception, messageId, offset);
      return SaveInboxMessageFailed;
    }
  }
}