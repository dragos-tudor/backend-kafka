using static Kafka.Operations.OperationState;

namespace Kafka.Operations;

partial class OperationsFuncs
{
  internal static async Task<string?> HandleInboxMessageAsync<TKey, TValue, TPayload, TSession>(
    InboxMessage<TKey, TPayload> message,
    IHandleInboxMessageServices<TKey, TValue, TPayload, TSession> services,
    CancellationToken cancellationToken)
  where TSession : IDisposable
  {
    var (model, handleError)  = await services.HandleInboxMessageAsync(message, cancellationToken);
    if (handleError is not null) return handleError;

    using var session = services.GetSession();
    await services.TransactSessionAsync(
      session,
      (session) => services.StoreSessionModelAsync(session, model),
      (session) => services.UpdateSessionInboxMessageStatusAsync(session, message, InboxMessageStatus.Handled),
      cancellationToken
    );
    return default;
  }

  internal static async ValueTask<(TData, OperationState)> HandleInboxMessageAsync<TServices, TData, TKey, TValue, TPayload, TSession>(
    TServices services,
    TData data,
    CancellationToken cancellationToken)
  where TSession : IDisposable
  where TServices : IHandleInboxMessageServices<TKey, TValue, TPayload, TSession>
  where TData : IHandleInboxMessageData<TKey, TValue, TPayload>
  {
    var inboxMessage = data.InboxMessage!;
    var domainError = await HandleInboxMessageAsync(inboxMessage, services, cancellationToken);
    data.DomainError = domainError;

    var _ = domainError is null?
      InstrumentHandleInboxMessage(services) :
      InstrumentHandleInboxMessageError(domainError, services);

    return domainError is null ?
      (data, HandledInboxMessageState) :
      (data, HandlingInboxMessageFailedState);
  }
}