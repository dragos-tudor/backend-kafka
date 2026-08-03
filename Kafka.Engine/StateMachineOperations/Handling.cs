
namespace Kafka.Engine;

partial class EngineFuncs
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
}