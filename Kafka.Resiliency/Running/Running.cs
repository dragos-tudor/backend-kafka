
namespace Kafka.Resiliency;

partial class ResiliencyFuncs
{
  public static async Task RunKafkaMessagesAsync<TKey, TValue, TPayload, TSession>(
    ConsumerConfig consumerConfig,
    ProducerConfig producerConfig,
    KafkaOptions kafkaOptions,
    IRunKafkaMessagesServices<TKey, TValue, TPayload, TSession> services,
    CancellationToken cancellationToken = default)
  where TSession: IDisposable
  {
    var clients = default(KafkaClients<TKey, TValue>);
    while (!cancellationToken.IsCancellationRequested)
    {
      try
      {
        clients = CreateKafkaClients<TKey, TValue>(consumerConfig, producerConfig);
      }
      catch (Exception exception)
      {
        LogCreateKafkaClientsFailed(services.GetLogger(), exception);
        await DelayTask(kafkaOptions.OperationTimeout, cancellationToken); //TODO: Consider using exponential backoff here instead of a fixed delay.
        continue;
      }

      using var producer = clients!.Producer;
      using var consumer = clients.Consumer;

      var processFailure = await ConsumeKafkaMessagesAsync(consumer, producer, kafkaOptions, services, cancellationToken);
      if (processFailure == ConsumingError.CriticalError)
      {
        await DelayTask(kafkaOptions.OperationTimeout, cancellationToken);
        continue;
      }
    }
  }

  public static async Task RunPeriodicJobAsync(
    string jobName,
    TimeSpan timerInterval,
    TimeSpan lockInterval,
    Func<CancellationToken, Task> work,
    IRunPeriodicJobServices services,
    CancellationToken cancellationToken = default)
  {
    using var timer = new PeriodicTimer(timerInterval);
    while (await timer.WaitForNextTickAsync(cancellationToken))
    {
      try
      {
        await using var handle = await services.TryAcquireLockAsync(jobName, lockInterval, cancellationToken);
        if (handle is not null)
          await work(cancellationToken);
      }
      catch (Exception exception)
      {
        LogPeriodicJobFailed(services.GetLogger(), exception, jobName);
      }
    }
  }
}