using static Kafka.Operations.Inbox.InsertingStates;

namespace Kafka.Operations.Inbox;

partial class InboxFuncs
{
  internal static async ValueTask<(TData, string)> InsertInboxMessageAsync<TServices, TData, TKey, TPayload>(
    TServices services,
    TData data,
    CancellationToken ct = default)
  where TServices : IInsertingServices<TKey, TPayload>
  where TData : IInsertingData<TKey, TPayload>
  {
    try {
      var message = RequireInboxMessage(data.InboxMessage);
      var messageInserted = await services.InsertInboxMessageAsync(message, ct);
      if (messageInserted is false)
      {
        data.InboxMessage = null;
        InstrumentIdempotentInboxMessage(message.MessageId, services);
        return (data, IdempotentInboxMessageState);
      }

      InstrumentInsertedInboxMessage(message.MessageId, services);
      return (data, InsertedInboxMessageState);
    }
    catch (OperationCanceledException) { return default; }
    catch (Exception ex) {
      InstrumentInsertInboxMessageError(data.InboxMessage?.MessageId, ex, services);
      return (data, InsertInboxMessageErrorState);
    }
  }
}