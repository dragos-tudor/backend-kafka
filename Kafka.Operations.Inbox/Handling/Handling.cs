using static Kafka.Operations.Inbox.HandlingStates;

namespace Kafka.Operations.Inbox;

partial class InboxFuncs
{
  internal static async ValueTask<(TData, string)> HandleInboxMessageAsync<TServices, TData, TKey, TPayload, TSession>(
    TServices services,
    TData data,
    CancellationToken ct = default)
  where TServices : IHandlingServices<TKey, TPayload, TSession>
  where TData : IHandlingData<TKey, TPayload>
  where TSession : IDisposable
  {
    try
    {
      var message = RequireInboxMessage(data.InboxMessage);
      RequireInboxMessagePayload(message.Payload);

      var (model, domainError) = await services.HandleInboxMessageAsync(message, ct);
      if (domainError is not null)
      {
        data.InboxMessageError = domainError;
        InstrumentHandleInboxMessageDomainError(message.MessageId, domainError, services);

        await services.UpdateInboxMessageAsync(message, message =>
          SetInboxMessageStatus(message, InboxMessageStatus.DeadLettering).
          SetInboxMessageLastError(domainError),
        ct);
        return (data, HandleInboxMessageDomainErrorState);
      }

      using var session = services.GetSession();
      var status = InboxMessageStatus.Handled;
      await services.TransactSessionAsync(
        session,
        (session) => services.StoreModelAsync(session, model),
        (session) => services.UpdateInboxMessageAsync(session, message,
          message => SetInboxMessageStatus(message, status)),
        ct
      );
      InstrumentHandledInboxMessage(message.MessageId, status, services);
      return (data, HandledInboxMessageState);

    }
    catch (OperationCanceledException) { return default; }
    catch (Exception ex)
    {
      data.InboxMessageError = ex.Message;
      InstrumentHandleInboxMessageTechnicalError(data.InboxMessage?.MessageId, ex, services);
      return (data, HandleInboxMessageTechnicalErrorState);
    }
  }
}