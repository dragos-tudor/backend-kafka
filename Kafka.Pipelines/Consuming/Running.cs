using static Kafka.Operations.Inbox.RedirectingStates;
using static Kafka.Operations.Inbox.InsertingStates;

namespace Kafka.Pipelines;


partial class PipelinesFuncs
{
  static Func<TServices, TData, string, KafkaOptions, RetryAttempts, CancellationToken, Task<(TData, string?)>>? NextConsumingStateMachine<TServices, TData, TKey, TValue, TPayload, TSession>
    (string? state)
    where TServices : IConsumingServices<TKey, TValue, TPayload, TSession>
    where TData : IConsumingData<TKey, TValue, TPayload>
    where TSession : IDisposable =>
      state switch
      {
        RedirectKafkaMessageErrorState => RetryRedirectConsumingStateMachineAsync<TServices, TData, TKey, TValue, TPayload, TSession>,
        InsertInboxMessageErrorState => RetryInsertConsumingStateMachineAsync<TServices, TData, TKey, TValue, TPayload, TSession>,
        RedirectKafkaMessageCircuitOpenState => null,
        InsertInboxMessageCircuitOpenState => null,
        not null => RunConsumingStateMachineAsync<TServices, TData, TKey, TValue, TPayload, TSession>,
        _ => null
      };

  static string? NextConsumingState(
    string state,
    RetryAttempts attempts,
    KafkaOptions kafkaOptions) =>
      state switch
      {
        RedirectKafkaMessageErrorState when attempts.RedirectAttempts < kafkaOptions.MaxRedirectRetries => RedirectKafkaMessageErrorState,
        RedirectKafkaMessageErrorState => RedirectKafkaMessageCircuitOpenState,
        InsertInboxMessageErrorState when attempts.InsertAttempts < kafkaOptions.MaxInsertRetries => InsertInboxMessageErrorState,
        InsertInboxMessageErrorState => InsertInboxMessageCircuitOpenState,
        _ => state
      };

  internal static async Task<(TData Data, string? State)> RunConsumingStateMachineAsync<TServices, TData, TKey, TValue, TPayload, TSession>(
      TServices services,
      TData data,
      string state,
      KafkaOptions kafkaOptions,
      RetryAttempts attempts = default,
      CancellationToken ct = default)
      where TServices : IConsumingServices<TKey, TValue, TPayload, TSession>
      where TData : IConsumingData<TKey, TValue, TPayload>
      where TSession : IDisposable
  {
    if (ct.IsCancellationRequested) return (data, state);

    var getStateAction = RouteConsumingStateMachine<TServices, TData, TKey, TValue, TPayload, TSession>(data);
    var (newData, newState) = await RunStateMachineAsync(services, data, state, getStateAction, ct);

    if (ReferenceEquals(newData, data)) return (newData, newState);

    var nextState = NextConsumingState(newState, attempts, kafkaOptions);
    var nextAction = NextConsumingStateMachine<TServices, TData, TKey, TValue, TPayload, TSession>(nextState);

    if (nextAction is null) return (newData, nextState);
    return await nextAction(services, newData, newState, kafkaOptions, attempts, ct);
  }
}