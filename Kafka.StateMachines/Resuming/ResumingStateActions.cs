
namespace Kafka.StateMachines;

partial class StateMachinesFuncs
{
  static ImmutableDictionary<string, StepAsync<TServices, TData, string>> GetResumingStateActions<TServices, TData, TKey, TValue, TPayload, TSession>()
    where TServices : IResumeInboxMessageServices<TKey, TValue, TPayload, TSession>
    where TData : IResumingStepData<TKey, TValue, TPayload>
    where TSession : IDisposable =>
      ImmutableDictionary<string, StepAsync<TServices, TData, string>>.Empty
        .Add(ResumingStates.ResumingNotStartedState, HandleInboxMessageAsync<TServices, TData, TKey, TValue, TPayload, TSession>)
        .Add(HandlingStates.HandleInboxMessageTechnicalErrorState, ScheduleInboxMessageAsync<TServices, TData, TKey, TValue, TPayload>)
        .Add(HandlingStates.HandleInboxMessageDomainErrorState, DispatchDeadLetterAsync<TServices, TData, TKey, TValue, TPayload>)
        .Add(Operations.Inbox.SchedulingStates.ScheduleInboxMessageExhaustedState, DispatchDeadLetterAsync<TServices, TData, TKey, TValue, TPayload>)
        .Add(Operations.Inbox.DispatchingStates.DispatchDeadLetterErrorState, DelayDeadLetterAsync<TServices, TData, TKey, TValue, TPayload>);
}