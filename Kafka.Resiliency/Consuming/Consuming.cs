
namespace Kafka.Resiliency;

partial class ResiliencyFuncs
{
  internal static async Task ConsumeKafkaMessagesLoopAsync<TKey, TValue, TPayload, TSession>(
    KafkaOptions kafkaOptions,
    IConsumingServices<TKey, TValue, TPayload, TSession> services,
    CancellationToken cancellationToken = default)
  where TSession: IDisposable
  {
    while (!cancellationToken.IsCancellationRequested)
    {
      var error = await ConsumeKafkaMessagesAsync(services, cancellationToken);
      if (error is not null)
      {
        await DelayTask(kafkaOptions.SessionTimeout, cancellationToken);
        continue;
      }
    }
  }
}