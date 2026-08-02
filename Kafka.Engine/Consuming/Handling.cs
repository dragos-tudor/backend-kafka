
namespace Kafka.Engine;

partial class EngineFuncs
{
  internal static async Task<string?> HandleInboxMessageAsync<TKey, TValue, TPayload, TSession>(
    InboxMessage<TKey, TPayload> message,
    IHandleInboxMessage<TKey, TValue, TPayload, TSession> services,
    CancellationToken cancellationToken)
  where TSession : IDisposable
  {
    var (model, error)  = await services.HandleInboxMessageAsync(message, cancellationToken);
    if (error is not null) return error;

    using var session = services.GetSession();
    await services.TransactInboxAsync(session,
      (session) => services.StoreSessionModel(session, model),
      (session) => services.UpdateSessionInboxMessageStatus(session, message, InboxMessageStatus.Handled),
      cancellationToken
    );
    return null;
  }
}