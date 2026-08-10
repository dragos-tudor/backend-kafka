
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
      using var consumer = services.GetConsumer();
      using var producer = services.GetProducer();
      var error = await ConsumeKafkaMessagesAsync<
        IConsumingServices<TKey, TValue, TPayload, TSession>,
        IConsumingData<TKey, TValue, TPayload>,
        TKey, TValue, TPayload, TSession>(services, cancellationToken);
      if (error is not null)
      {
        await DelayTask(kafkaOptions.SessionTimeout, cancellationToken);
        continue;
      }
    }
  }
}