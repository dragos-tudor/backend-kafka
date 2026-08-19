using static Kafka.Operations.Inbox.HandlingStates;
using static Kafka.Pipelines.ResumingStates;

namespace Kafka.Pipelines;

partial class PipelinesFuncs
{
  static StepAsync<TServices, TData, string>? GetResumingStateAction<TServices, TData, TKey, TPayload, TSession>(string state)
    where TServices : IResumingServices<TKey, TPayload, TSession>
    where TData : IResumingData<TKey, TPayload>
    where TSession : IDisposable =>
    state switch
    {
        ResumingNotStartedState =>
            HandleInboxMessageAsync<TServices, TData, TKey, TPayload, TSession>,

        HandleInboxMessageTechnicalErrorState =>
            ScheduleInboxMessageAsync<TServices, TData, TKey, TPayload>,

        _ => default
    };
}