
namespace Kafka.StateMachines;

partial class StateMachinesFuncs
{
  static ImmutableDictionary<string, StepAsync<TServices, TData, string>> GetRelayingStateActions<TServices, TData, TKey, TValue, TPayload>()
    where TServices : IRelayOutboxMessagesServices<TKey, TValue, TPayload>
    where TData : IRelayingStepData<TKey, TValue, TPayload> =>
      ImmutableDictionary<string, StepAsync<TServices, TData, string>>.Empty
        .Add(RelayingStates.RelayingNotStartedState, PublishOutboxMessageAsync<TServices, TData, TKey, TValue, TPayload>)
        .Add(PublishingStates.PublishOutboxMessageErrorState, ScheduleOutboxMessageAsync<TServices, TData, TKey, TValue, TPayload>)
        .Add(Operations.Outbox.SchedulingStates.ScheduleOutboxMessageErrorState, DispatchDeadLetterAsync<TServices, TData, TKey, TValue, TPayload>)
        .Add(Operations.Outbox.SchedulingStates.ScheduleOutboxMessageExhaustedState, DispatchDeadLetterAsync<TServices, TData, TKey, TValue, TPayload>)
        .Add(Operations.Outbox.DispatchingStates.DispatchDeadLetterErrorState, DelayDeadLetterAsync<TServices, TData, TKey, TValue, TPayload>);
}