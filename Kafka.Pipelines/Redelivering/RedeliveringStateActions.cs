
namespace Kafka.Pipelines;

partial class PipelinesFuncs
{
  static StepAsync<TServices, TData, string>? GetRedeliveringStateAction<TServices, TData, TKey, TValue, TPayload>(string state)
    where TServices : IRedeliveringServices<TKey, TValue, TPayload>
    where TData : IRedeliveringData<TKey, TValue, TPayload> =>
      state switch
      {
        _ => default
      };
}