
namespace Kafka.Operations.Outbox;

public interface IKafkaMessageProp<TKey, TValue> { Message<TKey, TValue>? KafkaMessage { get; set; } }

partial class OutboxFuncs
{
  static TValue RequireKafkaValue<TValue>(TValue? value) =>
    value ?? throw new InvalidOperationException(
      "ToKafkaValue returned null. Tombstone messages (null value) are not supported — " +
      "map to an explicit representation instead, or contact the library maintainers if tombstone support is needed.");
}
