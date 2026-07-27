
namespace Kafka;

partial class KafkaFuncs
{
  public static async Task ProcessKafkaMessagesAsync<TKey, TValue, TPayload>(
    ConsumerConfig consumerConfig,
    ProducerConfig producerConfig,
    KafkaOptions kafkaOptions,
    IProcessKafkaMessagesServices<TKey, TValue, TPayload> services,
    CancellationToken cancellationToken = default)
  {
    var logger = services.GetLogger("ProcessKafkaMessages");
    var processDelay = kafkaOptions.OperationTimeout;

    while (!cancellationToken.IsCancellationRequested)
    {
      var (clients, createFailure) = CreateKafkaClients<TKey, TValue>(consumerConfig, producerConfig, logger);
      if (createFailure != null)
      {
        await DelayTask(processDelay, cancellationToken);
        continue;
      }

      using var producer = clients.Producer;
      using var consumer = clients.Consumer;

      var processFailure = await ProcessKafkaMessagesAsync(services, producer, consumer, kafkaOptions, cancellationToken);
      if (processFailure == ProcessKafkaMessagesError.CriticalFailure)
      {
        await DelayTask(processDelay, cancellationToken);
        continue;
      }
    }
  }

  static async Task<ProcessKafkaMessagesError> ProcessKafkaMessagesAsync<TKey, TValue, TPayload>(
    IProcessKafkaMessagesServices<TKey, TValue, TPayload> services,
    IProducer<TKey, TValue> producer,
    IConsumer<TKey, TValue> consumer,
    KafkaOptions kafkaOptions,
    CancellationToken cancellationToken)
  {
    while (!cancellationToken.IsCancellationRequested)
    {
      var consumeFailure = await ConsumeKafkaMessageAsync(consumer, producer, kafkaOptions, services, cancellationToken);
      var processError = ToProcessKafkaMessagesError(consumeFailure);
      if (processError == ProcessKafkaMessagesError.CriticalFailure)
        return processError;
    }
    return ProcessKafkaMessagesError.None;
  }
}