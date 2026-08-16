using static Kafka.Operations.DeadLetter.MappingStates;

namespace Kafka.Operations.DeadLetter;

partial class DeadLetterFuncs
{
  internal static ValueTask<(TData, string)> MapDeadLetterMessage<TServices, TData, TKey, TValue, TPayload>(
    TServices services,
    TData data,
    CancellationToken ct = default)
  where TServices : IMappingServices<TKey, TValue, TPayload>
  where TData : IMappingData<TKey, TValue, TPayload>
  {
    try {
      var deadLetterMessage = RequireDeadLetterMessage(data.DeadLetterMessage);
      var payload = deadLetterMessage.Payload;
      var messageId = deadLetterMessage.MessageId;
      var correlationId = deadLetterMessage.CorrelationId;
      var kafkaValue = default(TValue?);

      if (payload is not null) {
        var (value, mapException) = TryRun(payload, services.ToKafkaValue);
        if (mapException is not null) {
          InstrumentMapDeadLetterMessagePayloadError(messageId, correlationId, mapException, services);
          return new ((data, MapDeadLetterMessagePayloadErrorState));
        }
        kafkaValue = RequireKafkaMessageValue(value);
      }

      var failureReason = deadLetterMessage.FailureReason;
      var topicPartitionOffset = DeserializeTopicPartitionOffset(deadLetterMessage.Metadata);
      var kafkaMessage = ToKafkaDeadLetter(deadLetterMessage, kafkaValue, topicPartitionOffset, failureReason, deadLetterMessage.Date);
      data.KafkaDeadLetter = kafkaMessage;

      SetTraceParentKafkaHeader(kafkaMessage.Headers, Activity.Current?.ParentId);
      InstrumentMappedDeadLetterMessage(GetKafkaMessageKey(kafkaMessage), messageId, correlationId, services);
      return new ((data, MappedDeadLetterMessageState));
    }
    catch (OperationCanceledException) { return default; }
    catch (Exception exception) {
      InstrumentMapDeadLetterMessageError(data.DeadLetterMessage?.MessageId, data.DeadLetterMessage?.CorrelationId, exception, services);
      return new ((data, MapDeadLetterMessageErrorState));
    }
  }
}