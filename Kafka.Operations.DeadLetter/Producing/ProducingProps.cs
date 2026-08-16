
namespace Kafka.Operations.DeadLetter;

public interface IProduceErrorProp { string? ProduceError { get; set; } }

partial class DeadLetterFuncs
{
  static Message<TKey, TValue?> RequireKafkaDeadLetter<TKey, TValue>(
    Message<TKey, TValue?>? message) =>
    message ?? throw new InvalidOperationException("Kafka dead letter message is required.");
}