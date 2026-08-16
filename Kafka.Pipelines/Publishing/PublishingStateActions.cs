
namespace Kafka.Pipelines;

partial class PipelinesFuncs
{
  static StepAsync<TServices, TData, string>? GetPublishingStateAction<TServices, TData, TKey, TValue, TPayload, TSession>(string state)
    where TServices : IPublishingServices<TKey, TValue, TPayload, TSession>
    where TData : IPublishingData<TKey, TValue, TPayload>
    where TSession : IDisposable =>
      state switch
      {
        _ => default
      };
}