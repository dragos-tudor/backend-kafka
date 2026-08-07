
namespace Kafka.StateMachines;

partial class StateMachinesFuncs
{
  static ImmutableDictionary<string, StepAsync<TServices, TData, string>> GetResumingStateActions<TServices, TData, TKey, TValue, TPayload, TSession>()
    where TServices : IResumeInboxMessageServices<TKey, TValue, TPayload, TSession>
    where TData : IResumingStepData<TKey, TValue, TPayload>
    where TSession : IDisposable =>
      ImmutableDictionary<string, StepAsync<TServices, TData, string>>.Empty
        .Add(NotStartedResumeState, HandleInboxMessageAsync<TServices, TData, TKey, TValue, TPayload, TSession>)
        .Add(HandleInboxMessageTechnicalErrorState, ScheduleInboxMessageAsync<TServices, TData, TKey, TValue, TPayload>)
        .Add(HandleInboxMessageDomainErrorState, DispatchDeadLetterAsync<TServices, TData, TKey, TValue, TPayload>)
        .Add(ScheduleInboxMessageExhaustedState, DispatchDeadLetterAsync<TServices, TData, TKey, TValue, TPayload>)
        .Add(DispatchDeadLetterErrorState, DelayDeadLetterAsync<TServices, TData, TKey, TValue, TPayload>);
}