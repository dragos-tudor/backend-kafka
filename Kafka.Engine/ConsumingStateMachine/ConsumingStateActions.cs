using static Kafka.Engine.ConsumingState;

namespace Kafka.Engine;

partial class EngineFuncs
{
  static IDictionary<ConsumingState, StepAsync<IConsumeKafkaMessageServices<TKey, TValue, TPayload, TSession>, IConsumingStepData<TKey, TValue, TPayload>, ConsumingState>>
    GetConsumingStateActions<TKey, TValue, TPayload, TSession>()
    where TSession : IDisposable =>
      GetConsumingStateActions<IConsumeKafkaMessageServices<TKey, TValue, TPayload, TSession>, IConsumingStepData<TKey, TValue, TPayload>, TKey, TValue, TPayload, TSession>();

  static IDictionary<ConsumingState, StepAsync<TService, TData, ConsumingState>>
    GetConsumingStateActions<TService, TData, TKey, TValue, TPayload, TSession>()
    where TService : IConsumeKafkaMessageServices<TKey, TValue, TPayload, TSession>
    where TData : IConsumingStepData<TKey, TValue, TPayload>
    where TSession : IDisposable =>
      ImmutableDictionary<ConsumingState, StepAsync<TService, TData, ConsumingState>>.Empty
        .Add(NotStartedState, CaptureKafkaMessageStepAsync<TService, TData, TKey, TValue, TPayload>)
        .Add(CapturedKafkaMessageState, InsertInboxMessageStepAsync<TService, TData, TKey, TValue, TPayload>)
        .Add(InsertedInboxMessageState, ApplyConsumerOffsetStepAsync<TService, TData, TKey, TValue, TPayload>)
        .Add(AppliedConsumerOffsetState, HandleInboxMessageStepAsync<TService, TData, TKey, TValue, TPayload, TSession>)
        .Add(HandlingInboxMessageFailedState, PublishDeadLetterStepAsync<TService, TData, TKey, TValue, TPayload>);
}