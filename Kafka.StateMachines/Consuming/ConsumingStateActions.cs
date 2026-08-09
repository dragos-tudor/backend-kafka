
namespace Kafka.StateMachines;

partial class StateMachinesFuncs
{
  static ImmutableDictionary<string, StepAsync<TServices, TData, string>> GetConsumingStateActions<TServices, TData, TKey, TValue, TPayload, TSession>()
    where TServices : IConsumeKafkaMessageServices<TKey, TValue, TPayload, TSession>
    where TData : IConsumingStepData<TKey, TValue, TPayload>
    where TSession : IDisposable =>
      ImmutableDictionary<string, StepAsync<TServices, TData, string>>.Empty
        .Add(ConsumingStates.ConsumingNotStartedState, CaptureKafkaMessage<TServices, TData, TKey, TValue, TPayload>)
        .Add(CapturingStates.CapturedKafkaMessageState, InsertInboxMessageAsync<TServices, TData, TKey, TValue, TPayload>)
        .Add(InsertingStates.InsertedInboxMessageState, OffsetConsumer<TServices, TData, TKey, TValue, TPayload>)
        .Add(InsertingStates.IdempotentInboxMessageState, OffsetConsumer<TServices, TData, TKey, TValue, TPayload>)
        .Add(OffsettingStates.OffsetConsumedState, HandleInboxMessageAsync<TServices, TData, TKey, TValue, TPayload, TSession>)
        .Add(HandlingStates.HandleInboxMessageDomainErrorState, DispatchDeadLetterAsync<TServices, TData, TKey, TValue, TPayload>);
}