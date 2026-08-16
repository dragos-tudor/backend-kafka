
namespace Kafka.Pipelines;

partial class PipelinesFuncs
{
  static StepAsync<TServices, TData, string>? GetRelayingStateAction<TServices, TData, TKey, TValue, TPayload>(string state)
    where TServices : IRelayingServices<TKey, TValue, TPayload>
    where TData : IRelayingData<TKey, TValue, TPayload> =>
      state switch
      {
        _ => default
      };
}