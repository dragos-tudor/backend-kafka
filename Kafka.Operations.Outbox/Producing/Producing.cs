using static Kafka.Operations.Outbox.ProducingStates;
using static Kafka.Messages.OutboxMessageStatus;

namespace Kafka.Operations.Outbox;

partial class OutboxFuncs
{
  internal static async ValueTask<(TData, string)> ProduceKafkaMessageAsync<TServices, TData, TKey, TValue, TPayload>(
    TServices services,
    TData data,
    CancellationToken ct = default)
  where TServices : IProducingServices<TKey, TValue, TPayload>
  where TData : IProducingData<TKey, TValue, TPayload>
  {
    try
    {
      var outboxMessage = data.OutboxMessage;
      var kafkaMessage = RequireKafkaMessage(data.KafkaMessage);
      var messageKey = GetKafkaMessageKey(kafkaMessage);
      var topic = services.GetOutboxTopic(outboxMessage);

      ProduceMessage(services.GetProducer(), topic, kafkaMessage,
        async (deliveryResult) => await ProduceKafkaMessageCallbackAsync(outboxMessage, kafkaMessage, deliveryResult, services));

      InstrumentProducingKafkaMessage(messageKey, topic, services);
      return new(data, ProducingKafkaMessageState);
    }
    catch (OperationCanceledException) { return default; }
    catch (KafkaException exception) when (exception.Error.IsFatal)
    {
      data.ProduceError = exception.Message;
      InstrumentProduceKafkaMessageCriticalError(GetKafkaMessageKey(data.KafkaMessage), exception, services);
      return new(data, ProduceKafkaMessageCriticalErrorState);
    }
    catch (Exception exception)
    {
      data.ProduceError = exception.Message;
      InstrumentProduceKafkaMessageError(GetKafkaMessageKey(data.KafkaMessage), exception, services);
      return new(data, ProduceKafkaMessageErrorState);
    }
  }

  static async Task<string> ProduceKafkaMessageCallbackAsync<TServices, TKey, TValue, TPayload>(
    OutboxMessage<TKey, TPayload> outboxMessage,
    Message<TKey, TValue> kafkaMessage,
    DeliveryReport<TKey, TValue> deliveryResult,
    TServices services)
    where TServices : IProducingServices<TKey, TValue, TPayload>
  {
    try
    {
      if (deliveryResult.Status != PersistenceStatus.Persisted)
      {
        InstrumentProduceCallbackDeliveryError(GetKafkaMessageKey(kafkaMessage), deliveryResult.Error.Reason, services);
        return ProduceCallbackDeliveryErrorState;
      }

      using var cts = new CancellationTokenSource(services.GetKafkaOptions().OperationTimeout);
      await services.UpdateOutboxMessageAsync(outboxMessage, message =>
          SetOutboxMessageStatus(message, Published), cts.Token);

      InstrumentProducedCallback(GetKafkaMessageKey(kafkaMessage), deliveryResult.TopicPartitionOffset, services);
      return ProducedCallbackState;
    }
    catch (Exception exception)
    {
      InstrumentProduceCallbackError(GetKafkaMessageKey(kafkaMessage), exception, services);
      return ProduceCallbackErrorState;
    }
  }
}