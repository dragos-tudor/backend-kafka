using static Kafka.Operations.DeadLetter.ConvertingStates;

namespace Kafka.Operations.DeadLetter;

partial class DeadLetterFuncs
{
  internal static ValueTask<(TData, string)> ConvertDeadLetterMessage<TServices, TData, TKey, TValue, TPayload>(
    TServices services,
    TData data,
    CancellationToken ct = default)
  where TServices : IConvertingServices<TKey, TValue, TPayload>
  where TData : IConvertingData<TKey, TPayload>
  {
    try {
      var inboxMessage = RequireInboxMessage(data.InboxMessage);
      var inboxMessageError = RequireInboxMessageError(data.InboxMessageError);
      var deadLetterMessage = ToDeadLetterMessage(inboxMessage, inboxMessageError, services.GetUtcDate());
      data.DeadLetterMessage = deadLetterMessage;

      InstrumentConvertedDeadLetterMessage(deadLetterMessage.MessageId, deadLetterMessage.CorrelationId, services);
      return new ((data, ConvertedDeadLetterMessageState));
    }
    catch (OperationCanceledException) { return default; }
    catch (Exception exception) {
      InstrumentConvertDeadLetterMessageError(data.InboxMessage?.MessageId, data.InboxMessage?.CorrelationId, exception, services);
      return new ((data, ConvertDeadLetterMessageErrorState));
    }
  }
}