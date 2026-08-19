using static Kafka.Pipelines.ConsumingStates;
using static Kafka.Operations.Inbox.CapturingStates;
using static Kafka.Operations.Inbox.MappingStates;
using static Kafka.Operations.Inbox.ValidatingStates;
using static Kafka.Operations.Inbox.RedirectingStates;
using static Kafka.Operations.Inbox.InsertingStates;
using static Kafka.Operations.Inbox.OffsettingStates;
using static Kafka.Operations.Inbox.HandlingStates;

namespace Kafka.Pipelines;

partial class PipelinesFuncs
{
  static StepAsync<TServices, TData, string>? GetNoKafkaMessageStateAction<TServices, TData, TKey, TValue, TPayload, TSession>(string state)
    where TServices : IConsumingServices<TKey, TValue, TPayload, TSession>
    where TData : IConsumingData<TKey, TValue, TPayload>
    where TSession : IDisposable =>
    state switch
    {
        ConsumingNotStartedState => CaptureKafkaMessage<TServices, TData, TKey, TValue>,
        CaptureKafkaMessageErrorState => OffsetConsumer<TServices, TData, TKey, TValue, TPayload>,
        _ => default
    };

  static StepAsync<TServices, TData, string>? GetNoInboxMessageStateAction<TServices, TData, TKey, TValue, TPayload, TSession>(string state)
    where TServices : IConsumingServices<TKey, TValue, TPayload, TSession>
    where TData : IConsumingData<TKey, TValue, TPayload>
    where TSession : IDisposable =>
    state switch
    {
        CapturedKafkaMessageState =>
            MapKafkaMessage<TServices, TData, TKey, TValue, TPayload>,

        MapKafkaMessageValueErrorState
            or MapKafkaMessageErrorState
            or ValidateInboxMessageDataErrorState
            or ValidateInboxMessagePayloadErrorState
            or RedirectKafkaMessageErrorState =>
            RedirectKafkaMessageAsync<TServices, TData, TKey, TValue, TPayload>,

        RedirectedKafkaMessageState or IdempotentInboxMessageState =>
            OffsetConsumer<TServices, TData, TKey, TValue, TPayload>,

        _ => default
    };

  static StepAsync<TServices, TData, string>? GetInboxMessageStateAction<TServices, TData, TKey, TValue, TPayload, TSession>(string state)
    where TServices : IConsumingServices<TKey, TValue, TPayload, TSession>
    where TData : IConsumingData<TKey, TValue, TPayload>
    where TSession : IDisposable =>
    state switch
    {
        MappedKafkaMessageState => ValidateInboxMessage<TServices, TData, TKey, TPayload>,
        ValidatedInboxMessageState => InsertInboxMessageAsync<TServices, TData, TKey, TPayload>,
        InsertedInboxMessageState => OffsetConsumer<TServices, TData, TKey, TValue, TPayload>,
        OffsetConsumedState => HandleInboxMessageAsync<TServices, TData, TKey, TPayload, TSession>,
        HandleInboxMessageTechnicalErrorState => ScheduleInboxMessageAsync<TServices, TData, TKey, TPayload>,
        InsertInboxMessageErrorState => InsertInboxMessageAsync<TServices, TData, TKey, TPayload>,
        _ => default
    };
}