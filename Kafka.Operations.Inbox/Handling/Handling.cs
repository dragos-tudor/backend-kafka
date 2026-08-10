using static Kafka.Operations.Inbox.HandlingStates;

namespace Kafka.Operations.Inbox;

partial class InboxFuncs
{
  internal static async ValueTask<(TData, string)> HandleInboxMessageAsync<TServices, TData, TKey, TValue, TPayload, TSession>(
    TServices services,
    TData data,
    CancellationToken ct = default)
  where TServices : IHandlingServices<TKey, TValue, TPayload, TSession>
  where TData : IHandlingData<TKey, TValue, TPayload>
  where TSession : IDisposable
  {
    var message = data.InboxMessage!;
    try {
      var (model, domainError)  = await services.HandleInboxMessageAsync(message, ct);
      if (domainError is not null) {
        data.HandleError = domainError;
        InstrumentHandleInboxMessageDomainError(message.MessageId, domainError, services);
        return (data, HandleInboxMessageDomainErrorState);
      }

      using var session = services.GetSession();
      await services.TransactSessionAsync(
        session,
        (session) => services.StoreModelAsync(session, model),
        (session) => services.UpdateIntegrationMessageAsync(session, message,
          message => message.SetInboxMessageStatus(InboxMessageStatus.Handled)),
        ct
      );

      InstrumentHandledInboxMessage(message.MessageId, services);
      return (data, HandledInboxMessageState);

    }
    catch (OperationCanceledException) { return default; }
    catch (Exception ex)
    {
      data.HandleError = ex.Message;
      InstrumentHandleInboxMessageTechnicalError(message.MessageId, ex.Message, services);
      return (data, HandleInboxMessageTechnicalErrorState);
    }
  }
}