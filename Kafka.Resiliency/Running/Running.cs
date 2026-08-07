
namespace Kafka.Resiliency;

partial class ResiliencyFuncs
{
  internal static async Task RunKafkaMessagesAsync<TServices, TData, TKey, TValue, TPayload, TSession>(
    ConsumerConfig consumerConfig,
    ProducerConfig producerConfig,
    KafkaOptions kafkaOptions,
    TServices services,
    CancellationToken cancellationToken = default)
  where TServices: IConsumeKafkaMessageServices<TKey, TValue, TPayload, TSession>
  where TData: IConsumingStepData<TKey, TValue, TPayload>
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
        LogCreateKafkaClientsError(services.GetLogger(), exception);
        await DelayTask(kafkaOptions.OperationTimeout, cancellationToken); //TODO: Consider using exponential backoff here instead of a fixed delay.
        continue;
      }

      using var producer = clients!.Producer;
      using var consumer = clients.Consumer;

      var error = await ConsumeKafkaMessagesAsync<TServices, TData, TKey, TValue, TPayload, TSession>(services, cancellationToken);
      if (error is not null)
      {
        await DelayTask(kafkaOptions.OperationTimeout, cancellationToken);
        continue;
      }
    }
  }

  internal static async Task RunPeriodicJobAsync(
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
        LogPeriodicJobError(services.GetLogger(), exception, jobName);
      }
    }
  }
}