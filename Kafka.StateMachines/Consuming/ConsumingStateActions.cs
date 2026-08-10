
namespace Kafka.StateMachines;

partial class StateMachinesFuncs
{
  static StepAsync<TServices, TData, string>? GetConsumingStateAction<TServices, TData, TKey, TValue, TPayload, TSession>(string state)
    where TServices : IConsumingServices<TKey, TValue, TPayload, TSession>
    where TData : IConsumingData<TKey, TValue, TPayload>
    where TSession : IDisposable =>
      state switch {
        ConsumingStates.ConsumingNotStartedState => CaptureKafkaMessage<TServices, TData, TKey, TValue>,
        CapturingStates.CapturedKafkaMessageState => InsertInboxMessageAsync<TServices, TData, TKey, TValue, TPayload>,
        InsertingStates.InsertedInboxMessageState => OffsetConsumer<TServices, TData, TKey, TValue, TPayload>,
        InsertingStates.IdempotentInboxMessageState => OffsetConsumer<TServices, TData, TKey, TValue, TPayload>,
        OffsettingStates.OffsetConsumedState => HandleInboxMessageAsync<TServices, TData, TKey, TValue, TPayload, TSession>,
        HandlingStates.HandleInboxMessageDomainErrorState => DispatchDeadLetterAsync<TServices, TData, TKey, TValue, TPayload>,
        _ => default
      };
}