
namespace Kafka.Operations.Outbox;

public interface IDeadLetterProp<TKey, TValue> { Message<TKey, TValue>? DeadLetter { get; set; } }

public interface IDispatchErrorProp { string? DispatchError { get; set; } }

public interface IOutboxMessageProp<TKey, TPayload> { OutboxMessage<TKey, TPayload>? OutboxMessage { get; set; } }

public interface IPipelineProp { string Pipeline { get; } }

public interface IPublishErrorProp { string? PublishError { get; set; } }

