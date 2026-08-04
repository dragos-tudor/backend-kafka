using static Kafka.Operations.OperationState;

namespace Kafka.StateMachines;

partial class StateMachinesFuncs
{
  static ImmutableDictionary<OperationState, StepAsync<IConsumeKafkaMessageServices<TKey, TValue, TPayload, TSession>, IConsumingStepData<TKey, TValue, TPayload>, OperationState>>
    GetConsumingStateActions<TKey, TValue, TPayload, TSession>()
    where TSession : IDisposable =>
      GetConsumingStateActions<IConsumeKafkaMessageServices<TKey, TValue, TPayload, TSession>, IConsumingStepData<TKey, TValue, TPayload>, TKey, TValue, TPayload, TSession>();

  static ImmutableDictionary<OperationState, StepAsync<TService, TData, OperationState>>
    GetConsumingStateActions<TService, TData, TKey, TValue, TPayload, TSession>()
    where TService : IConsumeKafkaMessageServices<TKey, TValue, TPayload, TSession>
    where TData : IConsumingStepData<TKey, TValue, TPayload>
    where TSession : IDisposable =>
      ImmutableDictionary<OperationState, StepAsync<TService, TData, OperationState>>.Empty
        .Add(NotStartedState, CaptureKafkaMessage<TService, TData, TKey, TValue, TPayload>)
        .Add(CapturedKafkaMessageState, InsertInboxMessageAsync<TService, TData, TKey, TValue, TPayload>)
        .Add(InsertedInboxMessageState, OffsetConsumer<TService, TData, TKey, TValue, TPayload>)
        .Add(IdempotentInboxMessageState, OffsetConsumer<TService, TData, TKey, TValue, TPayload>)
        .Add(OffsetConsumerState, HandleInboxMessageAsync<TService, TData, TKey, TValue, TPayload, TSession>)
        .Add(HandlingInboxMessageFailedState, PublishDeadLetterAsync<TService, TData, TKey, TValue, TPayload>);
}