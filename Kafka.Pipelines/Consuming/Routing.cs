
namespace Kafka.Pipelines;

partial class PipelinesFuncs
{
  static Func<string, StepAsync<TServices, TData, string>?> RouteConsumingStateMachine<TServices, TData, TKey, TValue, TPayload, TSession>(TData data)
    where TServices : IConsumingServices<TKey, TValue, TPayload, TSession>
    where TData : IConsumingData<TKey, TValue, TPayload>
    where TSession : IDisposable =>
    data switch
    {
        { KafkaMessage: null } => GetNoKafkaMessageStateAction<TServices, TData, TKey, TValue, TPayload, TSession>,
        { InboxMessage: null } => GetNoInboxMessageStateAction<TServices, TData, TKey, TValue, TPayload, TSession>,
        _ => GetInboxMessageStateAction<TServices, TData, TKey, TValue, TPayload, TSession>
    };
}