using static Kafka.Operations.DeadLetter.ConvertingStates;
using static Kafka.Pipelines.DeadLetteringStates;

namespace Kafka.Pipelines;

partial class PipelinesFuncs
{
  static StepAsync<TServices, TData, string>? GetDeadLetteringStateAction<TServices, TData, TKey, TPayload>(string state)
    where TServices : IDeadLetteringServices<TKey, TPayload>
    where TData : IDeadLetteringData<TKey, TPayload> =>
    state switch
    {
      DeadLetteringNotStartedState =>
        ConvertDeadLetterMessage<TServices, TData, TKey, TPayload>,

      ConvertedDeadLetterMessageState =>
        InsertDeadLetterMessageAsync<TServices, TData, TKey, TPayload>,

      _ => default
    };
}
