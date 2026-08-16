
namespace Kafka.Operations.Outbox;

public interface IOutboxMessageProp<TKey, TPayload> { OutboxMessage<TKey, TPayload> OutboxMessage { get; set; } }
