
namespace Kafka.StateMachines;

partial class StateMachinesFuncs
{
  static ImmutableDictionary<string, StepAsync<TServices, TData, string>> GetConsumingStateActions<TServices, TData, TKey, TValue, TPayload, TSession>()
    where TServices : IConsumeKafkaMessageServices<TKey, TValue, TPayload, TSession>
    where TData : IConsumingStepData<TKey, TValue, TPayload>
    where TSession : IDisposable =>
      ImmutableDictionary<string, StepAsync<TServices, TData, string>>.Empty
        .Add(NotStartedConsumeState, CaptureKafkaMessage<TServices, TData, TKey, TValue, TPayload>)
        .Add(CapturedKafkaMessageState, InsertInboxMessageAsync<TServices, TData, TKey, TValue, TPayload>)
        .Add(InsertedInboxMessageState, OffsetConsumer<TServices, TData, TKey, TValue, TPayload>)
        .Add(IdempotentInboxMessageState, OffsetConsumer<TServices, TData, TKey, TValue, TPayload>)
        .Add(OffsetConsumedState, HandleInboxMessageAsync<TServices, TData, TKey, TValue, TPayload, TSession>)
        .Add(HandleInboxMessageDomainErrorState, DispatchDeadLetterAsync<TServices, TData, TKey, TValue, TPayload>);
}