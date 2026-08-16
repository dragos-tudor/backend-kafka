using static Kafka.Operations.DeadLetter.ProducingStates;

namespace Kafka.Operations.DeadLetter;

partial class DeadLetterFuncs
{
  internal static ValueTask<(TData, string)> ProduceKafkaDeadLetter<TServices, TData, TKey, TValue>(
    TServices services,
    TData data,
    CancellationToken ct = default)
  where TServices : IProducingServices<TKey, TValue>
  where TData : IProducingData<TKey, TValue>
  {
    try
    {
      var kafkaMessage = RequireKafkaDeadLetter(data.KafkaDeadLetter);
      var messageKey = GetKafkaMessageKey(kafkaMessage);
      var topic = services.GetDeadLetterTopic();

      ProduceMessage(services.GetProducer(), topic, kafkaMessage,
        (deliveryResult) => ProduceKafkaDeadLetterCallback(kafkaMessage, deliveryResult, services));

      InstrumentProducingKafkaDeadLetter(messageKey, topic, services);
      return new((data, ProducingKafkaDeadLetterState));
    }
    catch (OperationCanceledException) { return default; }
    catch (KafkaException exception) when (exception.Error.IsFatal)
    {
      data.ProduceError = exception.Message;
      InstrumentProduceKafkaDeadLetterCriticalError(GetKafkaMessageKey(data.KafkaDeadLetter), exception, services);
      return new((data, ProduceKafkaDeadLetterCriticalErrorState));
    }
    catch (Exception exception)
    {
      data.ProduceError = exception.Message;
      InstrumentProduceKafkaDeadLetterError(GetKafkaMessageKey(data.KafkaDeadLetter), exception, services);
      return new((data, ProduceKafkaDeadLetterErrorState));
    }
  }

  static string ProduceKafkaDeadLetterCallback<TServices, TKey, TValue>(
    Message<TKey, TValue?> kafkaMessage,
    DeliveryReport<TKey, TValue?> deliveryResult,
    TServices services)
    where TServices : IInstrumentationServices
  {
    try
    {
      if (deliveryResult.Status != PersistenceStatus.Persisted)
      {
        InstrumentProduceDeadLetterCallbackDeliveryError(GetKafkaMessageKey(kafkaMessage), deliveryResult.Error.Reason, services);
        return ProduceDeadLetterCallbackDeliveryErrorState;
      }

      InstrumentProducedDeadLetterCallback(GetKafkaMessageKey(kafkaMessage), deliveryResult.TopicPartitionOffset, services);
      return ProducedDeadLetterCallbackState;
    }
    catch (Exception exception)
    {
      InstrumentProduceDeadLetterCallbackError(GetKafkaMessageKey(kafkaMessage), exception, services);
      return ProduceDeadLetterCallbackErrorState;
    }
  }
}