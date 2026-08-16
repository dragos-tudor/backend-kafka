
namespace Kafka.Pipelines;

internal interface IRedeliveringData<TKey, TValue, TPayload>:
  Operations.DeadLetter.IMappingData<TKey, TValue, TPayload>,
  Operations.DeadLetter.IProducingData<TKey, TValue>,
  Operations.DeadLetter.ISchedulingData<TKey, TPayload>;

internal sealed class RedeliveringData<TKey, TValue, TPayload>:
  IRedeliveringData<TKey, TValue, TPayload>
{
  public Message<TKey, TValue>? KafkaMessage { get; set; }
  public required DeadLetterMessage<TKey, TPayload>? DeadLetterMessage { get; set; }
  public Message<TKey, TValue?>? KafkaDeadLetter { get; set; }
  public string? ProduceError { get; set; }
}

partial class PipelinesFuncs
{
  internal static IRedeliveringData<TKey, TValue, TPayload> CreateRedeliveringData<TKey, TValue, TPayload>(
    DeadLetterMessage<TKey, TPayload> message) =>
    new RedeliveringData<TKey, TValue, TPayload>
    {
      DeadLetterMessage = message
    };
}