using static Kafka.Operations.Inbox.MappingStates;

namespace Kafka.Operations.Inbox;

partial class InboxFuncs
{
  internal static ValueTask<(TData, string)> MapKafkaMessage<TServices, TData, TKey, TValue, TPayload>(
    TServices services,
    TData data,
    CancellationToken ct = default)
  where TServices : IMappingServices<TKey, TValue, TPayload>
  where TData : IMappingData<TKey, TValue, TPayload>
  {
    try {
      var kafkaMessage = RequireKafkaMessage(data.KafkaMessage);
      var topicPartitionOffset = RequireTopicPartitionOffset(data.TopicPartitionOffset);
      var messageKey = GetKafkaMessageKey(kafkaMessage);
      var value = GetKafkaMessageValue(kafkaMessage);

      var (payload, mapException) = TryRun(value, services.ToInboxPayload);
      var status = GetInboxMessageStatus(payload);
      var inboxMessage = ToInboxMessage(kafkaMessage, topicPartitionOffset, payload, services.GetUtcDate(), status, mapException?.ToString());
      data.InboxMessage = inboxMessage;

      if (mapException is not null) {
        data.InboxMessageError = mapException.Message;
        InstrumentMapKafkaMessageValueError(messageKey, inboxMessage.MessageId, inboxMessage.CorrelationId, mapException, services);
        return new ((data, MapKafkaMessageValueErrorState));
      }

      InstrumentMappedKafkaMessage(messageKey, inboxMessage.MessageId, inboxMessage.CorrelationId, services);
      return new ((data, MappedKafkaMessageState));
    }
    catch (OperationCanceledException) { return default; }
    catch (Exception exception) {
      data.InboxMessageError = exception.Message;
      InstrumentMapKafkaMessageError(GetKafkaMessageKey(data.KafkaMessage), exception, services);
      return data.InboxMessage is not null?
        new ((data, MapKafkaMessageWithInboxErrorState)) :
        new ((data, MapKafkaMessageWithoutInboxErrorState));
    }
  }
}