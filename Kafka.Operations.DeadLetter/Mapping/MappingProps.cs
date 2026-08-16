
namespace Kafka.Operations.DeadLetter;

public interface IKafkaDeadLetterProp<TKey, TValue> { Message<TKey, TValue?>? KafkaDeadLetter { get; set; } }

partial class DeadLetterFuncs
{
  internal static TValue RequireKafkaMessageValue<TValue>(TValue? value) =>
    value is not null ?
      value :
      throw new InvalidOperationException("Kafka message value is required.");
}
