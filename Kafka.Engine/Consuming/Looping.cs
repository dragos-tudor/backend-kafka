using static Kafka.Engine.ConsumingState;

namespace Kafka.Engine;

partial class EngineFuncs
{
  internal static async Task<ConsumingError> ConsumeKafkaMessagesAsync<TKey, TValue, TPayload, TSession>(
    IConsumer<TKey, TValue> consumer,
    IProducer<TKey, TValue> producer,
    KafkaOptions kafkaOptions,
    IConsumeKafkaMessagesServices<TKey, TValue, TPayload, TSession> services,
    CancellationToken cancellationToken)
  where TSession : IDisposable
  {
    while (!cancellationToken.IsCancellationRequested)
    {
      ConsumingState currentState = ConsumingMessageState;
      try
      {
        var states = ConsumeKafkaMessageAsync(consumer, producer, kafkaOptions, services, cancellationToken);
        await foreach (var state in states)
          currentState = state;
      }
      catch (OperationCanceledException) { return ConsumingError.None; }
      catch (Exception exception)
      {
        LogConsumeKafkaMessageFailed(services.GetLogger(), exception, currentState);
        var error = ToConsumingError(currentState);
        if (error != ConsumingError.None)
          return error;
      }
    }
    return ConsumingError.None;
  }
}