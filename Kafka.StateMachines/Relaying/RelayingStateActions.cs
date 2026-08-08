
namespace Kafka.StateMachines;

partial class StateMachinesFuncs
{
  static ImmutableDictionary<string, StepAsync<TServices, TData, string>> GetRelayingStateActions<TServices, TData, TKey, TValue, TPayload>()
    where TServices : IRelayOutboxMessagesServices<TKey, TValue, TPayload>
    where TData : IRelayingStepData<TKey, TValue, TPayload> =>
      ImmutableDictionary<string, StepAsync<TServices, TData, string>>.Empty
        .Add(RelayingNotStartedState, PublishOutboxMessageAsync<TServices, TData, TKey, TValue, TPayload>)
        .Add(PublishOutboxMessageErrorState, ScheduleOutboxMessageAsync<TServices, TData, TKey, TValue, TPayload>)
        .Add(ScheduleOutboxMessageErrorState, DispatchDeadLetterAsync<TServices, TData, TKey, TValue, TPayload>)
        .Add(ScheduleOutboxMessageExhaustedState, DispatchDeadLetterAsync<TServices, TData, TKey, TValue, TPayload>)
        .Add(OutboxFuncs.DispatchDeadLetterErrorState, DelayDeadLetterAsync<TServices, TData, TKey, TValue, TPayload>);
}