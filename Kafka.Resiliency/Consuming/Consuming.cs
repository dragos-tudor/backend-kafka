
namespace Kafka.Resiliency;

partial class ResiliencyFuncs
{
  internal static async Task ConsumeKafkaMessagesLoopAsync<TServices, TData, TKey, TValue, TPayload, TSession>(
    KafkaOptions kafkaOptions,
    TServices services,
    CancellationToken cancellationToken = default)
  where TServices: IConsumeKafkaMessageServices<TKey, TValue, TPayload, TSession>
  where TData: IConsumingStepData<TKey, TValue, TPayload>
  where TSession: IDisposable
  {
    while (!cancellationToken.IsCancellationRequested)
    {
      var error = await ConsumeKafkaMessagesAsync<TServices, TData, TKey, TValue, TPayload, TSession>(services, cancellationToken);
      if (error is not null)
      {
        await DelayTask(kafkaOptions.OperationTimeout, cancellationToken);
        continue;
      }
    }
  }
}