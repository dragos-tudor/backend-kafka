using static Kafka.Pipelines.PublishingStates;

namespace Kafka.Pipelines;

partial class PipelinesFuncs
{
  public static async Task<string?> PublishOutboxMessageAsync<TServices, TData, TKey, TValue, TPayload, TSession, TModel>(
    OutboxMessage<TKey, TPayload> message,
    TModel model,
    TServices services,
    CancellationToken ct = default)
  where TServices : IPublishingServices<TKey, TValue, TPayload, TSession>
  where TData : IPublishingData<TKey, TValue, TPayload>
  where TSession : IDisposable
  {
    using var activity = CreateDefaultActivity(services.GetActivitySource(), "publish.outbox.message", ActivityKind.Internal);
    using var logScope = CreateComponentLogScope(services.GetLogger(), activity, "publish.outbox.message");

    var initialData = (TData)CreatePublishingData<TKey, TValue, TPayload, TModel>(message, model);
    var initialState = PublishingNotStartedState;
    var getStateAction = GetPublishingStateAction<TServices, TData, TKey, TValue, TPayload, TSession>;

    var (_, lastState) = await RunStateMachineAsync(services, initialData, initialState, getStateAction, ct);
    if (PublishingCriticalStates.Contains(lastState))
    {
      InstrumentPublishOutboxMessageCriticalError(lastState, services);
      return PublishingCriticalErrorState;
    }
    InstrumentPublishedOutboxMessage(message.MessageId, lastState, services);
    return null;
  }
}