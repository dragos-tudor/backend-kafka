
namespace Kafka.Inbox;

partial class InboxFuncs
{
  internal static async ValueTask<(TData, string)> HandleInboxMessageAsync<TServices, TData, TKey, TValue, TPayload, TSession>(
    TServices services,
    TData data,
    CancellationToken ct = default)
  where TServices : IHandleInboxMessageServices<TKey, TValue, TPayload, TSession>
  where TData : IHandleInboxMessageData<TKey, TValue, TPayload>
  where TSession : IDisposable
  {
    var message = data.InboxMessage!;
    try {
      var (model, error)  = await services.HandleInboxMessageAsync(message, ct);
      var state = GetHandleInboxMessageState(model, error);

      if (state == HandleInboxMessageDomainErrorState) {
        data.HandleError = error;
        InstrumentHandleInboxMessageDomainError(message.MessageId, error!, services);
        return (data, state);
      }

      using var session = services.GetSession();
      await services.TransactSessionAsync(
        session,
        (session) => services.StoreModelAsync(session, model),
        (session) => services.UpdateIntegrationMessageAsync(session, message, InboxMessageStatus.Handled),
        ct
      );

      InstrumentHandledInboxMessage(message.MessageId, services);
      return (data, state);

    }
    catch (Exception ex)
    {
      data.HandleError = ex.Message;
      InstrumentHandleInboxMessageTechnicalError(message.MessageId, ex.Message, services);
      return (data, HandleInboxMessageTechnicalErrorState);
    }
  }
}