
namespace Kafka.Pipelines;

partial class PipelinesFuncs
{
  static StepAsync<TServices, TData, string>? GetRelayingStateAction<TServices, TData, TKey, TValue, TPayload>(string state)
    where TServices : IRelayingServices<TKey, TValue, TPayload>
    where TData : IRelayingData<TKey, TValue, TPayload> =>
      state switch
      {
        RelayingStates.RelayingNotStartedState => PublishOutboxMessageAsync<TServices, TData, TKey, TValue, TPayload>,
        PublishingStates.PublishOutboxMessageErrorState => ScheduleOutboxMessageAsync<TServices, TData, TKey, TValue, TPayload>,
        Operations.Outbox.SchedulingStates.ScheduleOutboxMessageErrorState => DispatchDeadLetterAsync<TServices, TData, TKey, TValue, TPayload>,
        Operations.Outbox.SchedulingStates.ScheduleOutboxMessageExhaustedState => DispatchDeadLetterAsync<TServices, TData, TKey, TValue, TPayload>,
        Operations.Outbox.DispatchingStates.DispatchDeadLetterErrorState => DelayDeadLetterAsync<TServices, TData, TKey, TValue, TPayload>,
        _ => default
      };
}