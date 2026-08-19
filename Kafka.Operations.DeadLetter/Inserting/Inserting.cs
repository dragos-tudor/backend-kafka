using static Kafka.Operations.DeadLetter.InsertingStates;

namespace Kafka.Operations.DeadLetter;

partial class DeadLetterFuncs
{
  internal static async ValueTask<(TData, string)> InsertDeadLetterMessageAsync<TServices, TData, TKey, TPayload>(
    TServices services,
    TData data,
    CancellationToken ct = default)
  where TServices : IInsertingServices<TKey, TPayload>
  where TData : IInsertingData<TKey, TPayload>
  {
    try {
      var deadLetterMessage = RequireDeadLetterMessage(data.DeadLetterMessage);
      var inboxMessage = RequireInboxMessage(data.InboxMessage);

      var deadLetterIdempotent = await services.InsertDeadLetterMessageAsync(deadLetterMessage, ct);
      await services.UpdateInboxMessageAsync(inboxMessage, message =>
        SetInboxMessageStatus(message, InboxMessageStatus.DeadLettered), ct);

      if (deadLetterIdempotent is false)
      {
        data.DeadLetterMessage = null;
        InstrumentIdempotentDeadLetterMessage(deadLetterMessage.MessageId, services);
        return (data, IdempotentDeadLetterMessageState);
      }

      InstrumentInsertedDeadLetterMessage(deadLetterMessage.MessageId, services);
      return (data, InsertedDeadLetterMessageState);
    }
    catch (OperationCanceledException) { return default; }
    catch (Exception ex) {
      InstrumentInsertDeadLetterMessageError(data.DeadLetterMessage?.MessageId, ex, services);
      return (data, InsertDeadLetterMessageErrorState);
    }
  }
}