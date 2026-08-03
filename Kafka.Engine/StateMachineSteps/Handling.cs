using static Kafka.Engine.ConsumingState;

namespace Kafka.Engine;

partial class EngineFuncs
{
  internal static async ValueTask<ConsumingState> HandleInboxMessageStepAsync<TService, TData, TKey, TValue, TPayload, TSession>(
    StepContext<TService, TData> ctx,
    CancellationToken cancellationToken)
  where TSession : IDisposable
  where TService : IHandleInboxMessageServices<TKey, TValue, TPayload, TSession>
  where TData : IHandleInboxMessageData<TKey, TValue, TPayload>
  {
    var services = ctx.Services;
    var data = ctx.Data;

    var domainError = await HandleInboxMessageAsync(data.Message!, services, cancellationToken);
    data.DomainError = domainError;

    var _ = domainError is null?
      InstrumentHandleInboxMessageStep(ctx.Activity, services) :
      InstrumentHandleInboxMessageErrorStep(ctx.Activity, domainError, services);

    return domainError is null ?
      HandledInboxMessageState :
      HandlingInboxMessageFailedState;
  }
}