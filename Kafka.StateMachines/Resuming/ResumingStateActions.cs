
namespace Kafka.StateMachines;

partial class StateMachinesFuncs
{
  static StepAsync<TServices, TData, string>? GetResumingStateAction<TServices, TData, TKey, TValue, TPayload, TSession>(string state)
    where TServices : IResumingServices<TKey, TValue, TPayload, TSession>
    where TData : IResumingData<TKey, TValue, TPayload>
    where TSession : IDisposable =>
      state switch
      {
        ResumingStates.ResumingNotStartedState => HandleInboxMessageAsync<TServices, TData, TKey, TValue, TPayload, TSession>,
        HandlingStates.HandleInboxMessageTechnicalErrorState => ScheduleInboxMessageAsync<TServices, TData, TKey, TValue, TPayload>,
        HandlingStates.HandleInboxMessageDomainErrorState => DispatchDeadLetterAsync<TServices, TData, TKey, TValue, TPayload>,
        Operations.Inbox.SchedulingStates.ScheduleInboxMessageExhaustedState => DispatchDeadLetterAsync<TServices, TData, TKey, TValue, TPayload>,
        Operations.Inbox.DispatchingStates.DispatchDeadLetterErrorState => DelayDeadLetterAsync<TServices, TData, TKey, TValue, TPayload>,
        _ => default
      };
}