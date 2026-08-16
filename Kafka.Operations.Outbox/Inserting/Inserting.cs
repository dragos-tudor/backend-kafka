using static Kafka.Operations.Outbox.InsertingStates;

namespace Kafka.Operations.Outbox;

partial class OutboxFuncs
{
  internal static async ValueTask<(TData, string)> InsertOutboxMessageAsync<TServices, TData, TKey, TPayload, TSession>(
    TServices services,
    TData data,
    CancellationToken ct = default)
  where TServices : IInsertingServices<TKey, TPayload, TSession>
  where TData : IInsertingData<TKey, TPayload>
  where TSession : IDisposable
  {
    try {
      var message = data.OutboxMessage;
      var model = data.Model;

      using var session = services.GetSession();
      SetOutboxMessageStatus(message, OutboxMessageStatus.Pending);
      await services.TransactSessionAsync(
        session,
        (session) => services.StoreModelAsync(session, model),
        (session) => services.InsertOutboxMessageAsync(session, message, ct),
        ct
      );

      InstrumentInsertedOutboxMessage(message.MessageId, message.Status, services);
      return (data, InsertedOutboxMessageState);
    }
    catch (OperationCanceledException) { return default; }
    catch (Exception ex) {
      InstrumentInsertOutboxMessageError(data.OutboxMessage.MessageId, ex, services);
      return (data, InsertOutboxMessageErrorState);
    }
  }
}