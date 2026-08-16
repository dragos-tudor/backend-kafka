
namespace Kafka.Operations.Outbox;

public interface IProduceErrorProp { string? ProduceError { get; set; } }

partial class OutboxFuncs
{
  static Message<TKey, TValue> RequireKafkaMessage<TKey, TValue>(
    Message<TKey, TValue>? message) =>
    message ?? throw new InvalidOperationException("Kafka message is required.");
}