
namespace Kafka.Pipelines;

partial class PipelinesFuncs
{
  static StepAsync<TServices, TData, string>? GetResumingStateAction<TServices, TData, TKey, TValue, TPayload, TSession>(string state)
    where TServices : IResumingServices<TKey, TValue, TPayload, TSession>
    where TData : IResumingData<TKey, TValue, TPayload>
    where TSession : IDisposable =>
      state switch
      {
        _ => default
      };
}