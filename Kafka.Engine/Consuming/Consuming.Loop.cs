
using static Kafka.Engine.ConsumingState;

namespace Kafka.Engine;

partial class EngineFuncs
{
  internal static async Task<ConsumingError> ConsumeKafkaMessagesAsync<TKey, TValue, TPayload, TSession>(
    IConsumer<TKey, TValue> consumer,
    IProducer<TKey, TValue> producer,
    KafkaOptions kafkaOptions,
    IConsumeKafkaMessages<TKey, TValue, TPayload, TSession> services,
    CancellationToken cancellationToken)
  where TSession : IDisposable
  {
    using var activity = CreateComponentActivity(services.GetActivitySource(), "consume-kafka-messages", ActivityKind.Internal, KafkaConsumer);
    using var logScope = CreateLogScopeForActivity(services.GetLogger(), activity, KafkaConsumer);
    var metricCounters = services.GetMetricCounters();

    while (!cancellationToken.IsCancellationRequested)
    {
      var currentState = ConsumingMessageState;
      try
      {
        var states = ConsumeKafkaMessageAsync(consumer, producer, kafkaOptions, services, cancellationToken);
        await foreach (var state in states)
          currentState = state;
        LogConsumedKafkaMessage(services.GetLogger(), currentState);
      }
      catch (OperationCanceledException) { return ConsumingError.None; }
      catch (Exception exception)
      {
        LogConsumeKafkaMessageFailed(services.GetLogger(), exception, currentState);
        metricCounters[MetricCounterTypes.ConsumingErrors].Add(1);

        var error = ToConsumingError(currentState);
        if (error != ConsumingError.None)
          return error;
      }
    }
    return ConsumingError.None;
  }
}