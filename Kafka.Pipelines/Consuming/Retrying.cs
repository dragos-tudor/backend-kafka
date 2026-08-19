
namespace Kafka.Pipelines;

internal readonly record struct RetryAttempts(int RedirectAttempts = 0, int InsertAttempts = 0);

partial class PipelinesFuncs
{
  static Task<(TData, string?)> RetryRedirectConsumingStateMachineAsync<TServices, TData, TKey, TValue, TPayload, TSession>(
      TServices services,
      TData data,
      string state,
      KafkaOptions kafkaOptions,
      RetryAttempts attempts,
      CancellationToken ct = default)
      where TServices : IConsumingServices<TKey, TValue, TPayload, TSession>
      where TData : IConsumingData<TKey, TValue, TPayload>
      where TSession : IDisposable =>
        RetryConsumingStateMachineAsync<TServices, TData, TKey, TValue, TPayload, TSession>(
            services, data, state, kafkaOptions, attempts with { RedirectAttempts = attempts.RedirectAttempts + 1 }, kafkaOptions.RedirectRetryDelay, ct);

  static Task<(TData, string?)> RetryInsertConsumingStateMachineAsync<TServices, TData, TKey, TValue, TPayload, TSession>(
      TServices services,
      TData data,
      string state,
      KafkaOptions kafkaOptions,
      RetryAttempts attempts,
      CancellationToken ct = default)
    where TServices : IConsumingServices<TKey, TValue, TPayload, TSession>
    where TData : IConsumingData<TKey, TValue, TPayload>
    where TSession : IDisposable =>
        RetryConsumingStateMachineAsync<TServices, TData, TKey, TValue, TPayload, TSession>(
            services, data, state, kafkaOptions, attempts with { InsertAttempts = attempts.InsertAttempts + 1 }, kafkaOptions.InsertRetryDelay, ct);


  static async Task<(TData, string?)> RetryConsumingStateMachineAsync<TServices, TData, TKey, TValue, TPayload, TSession>(
      TServices services,
      TData data,
      string state,
      KafkaOptions kafkaOptions,
      RetryAttempts attempts,
      TimeSpan delay,
      CancellationToken ct = default)
      where TServices : IConsumingServices<TKey, TValue, TPayload, TSession>
      where TData : IConsumingData<TKey, TValue, TPayload>
      where TSession : IDisposable
  {
      await DelayTask(delay, ct);
      return await RunConsumingStateMachineAsync<TServices, TData, TKey, TValue, TPayload, TSession>(
          services, data, state, kafkaOptions, attempts, ct);
  }
}