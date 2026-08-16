using static Kafka.Operations.Inbox.RedirectingStates;

namespace Kafka.Operations.Inbox;

partial class InboxFuncs
{
  internal static async ValueTask<(TData, string)> RedirectKafkaMessageAsync<TServices, TData, TKey, TValue, TPayload>(
    TServices services,
    TData data,
    CancellationToken ct = default)
  where TServices : IRedirectingServices<TKey, TValue, TPayload>
  where TData : IRedirectingData<TKey, TValue, TPayload>
  {
    try
    {
      var kafkaMessage = RequireKafkaMessage(data.KafkaMessage);
      var topicPartitionOffset = RequireTopicPartitionOffset(data.TopicPartitionOffset);
      var inboxMessageError = RequireInboxMessageError(data.InboxMessageError);

      var kafkaDeadLetter = ToKafkaDeadLetter(kafkaMessage, topicPartitionOffset, inboxMessageError, services.GetUtcDate());
      var topic = services.GetKafkaDeadLetterTopic(kafkaMessage);
      var messageKey = GetKafkaMessageKey(kafkaDeadLetter);

      var deliveryResult = await PublishMessageAsync(services.GetProducer(), topic, kafkaDeadLetter, ct);
      var status = deliveryResult.Status switch {
        PersistenceStatus.Persisted => RedirectedKafkaMessageState,
        PersistenceStatus.PossiblyPersisted => RedirectKafkaMessageAmbiguousState,
        _ => RedirectKafkaMessageDeliveryErrorState
};
      if (status != RedirectedKafkaMessageState) {
        InstrumentRedirectKafkaMessageDeliveryWarning(messageKey, "Kafka message not or possibly redirected", services);
        return (data, status);
      }

      InstrumentRedirectedKafkaMessage(messageKey, topic, deliveryResult.TopicPartitionOffset, services);
      return (data, status);
    }
    catch (OperationCanceledException) { return default; }
    catch (KafkaException exception) when (exception.Error.IsFatal)
    {
      InstrumentRedirectKafkaMessageCriticalError(GetKafkaMessageKey(data.KafkaMessage), exception, services);
      return new(data, RedirectKafkaMessageCriticalErrorState);
    }
    catch (Exception exception)
    {
      InstrumentRedirectKafkaMessageError(GetKafkaMessageKey(data.KafkaMessage), exception, services);
      return new(data, RedirectKafkaMessageErrorState);
    }
  }
}