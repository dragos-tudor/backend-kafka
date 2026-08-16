
namespace Kafka.Pipelines;

partial class PipelinesFuncs
{
  static StepAsync<TServices, TData, string>? GetConsumingStateAction<TServices, TData, TKey, TValue, TPayload, TSession>(string state)
    where TServices : IConsumingServices<TKey, TValue, TPayload, TSession>
    where TData : IConsumingData<TKey, TValue, TPayload>
    where TSession : IDisposable =>
      state switch {
        _ => default
      };
}