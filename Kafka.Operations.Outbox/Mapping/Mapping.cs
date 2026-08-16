using static Kafka.Operations.Outbox.MappingStates;

namespace Kafka.Operations.Outbox;

partial class OutboxFuncs
{
  internal static ValueTask<(TData, string)> MapOutboxMessage<TServices, TData, TKey, TValue, TPayload>(
    TServices services,
    TData data,
    CancellationToken ct = default)
  where TServices : IMappingServices<TKey, TValue, TPayload>
  where TData : IMappingData<TKey, TValue, TPayload>
  {
    try {
      var outboxMessage = data.OutboxMessage;
      var messageId = outboxMessage.MessageId;
      var correlationId = outboxMessage.CorrelationId;

      var (value, mapException) = TryRun(outboxMessage.Payload, services.ToKafkaValue);
      if (mapException is not null) {
        InstrumentMapOutboxMessagePayloadError(messageId, correlationId, mapException, services);
        return new ((data, MapOutboxMessagePayloadErrorState));
      }

      var kafkaValue = RequireKafkaValue(value);
      var kafkaMessage = ToKafkaMessage(outboxMessage, kafkaValue, outboxMessage.Date);
      data.KafkaMessage = kafkaMessage;

      SetTraceParentKafkaHeader(kafkaMessage.Headers, Activity.Current?.ParentId);
      InstrumentMappedOutboxMessage(GetKafkaMessageKey(kafkaMessage), messageId, correlationId, services);
      return new ((data, MappedOutboxMessageState));
    }
    catch (OperationCanceledException) { return default; }
    catch (Exception exception) {
      InstrumentMapOutboxMessageError(data.OutboxMessage.MessageId, data.OutboxMessage.CorrelationId, exception, services);
      return new ((data, MapOutboxMessageErrorState));
    }
  }
}