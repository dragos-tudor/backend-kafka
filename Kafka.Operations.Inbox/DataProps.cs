
namespace Kafka.Operations.Inbox;

public interface IDeadLetterProp<TKey, TValue> { Message<TKey, TValue>? DeadLetter { get; set; } }

public interface IDispatchErrorProp { string? DispatchError { get; set; } }

public interface IHandleErrorProp { string? HandleError { get; set; } }

public interface IInboxMessageProp<TKey, TPayload> { InboxMessage<TKey, TPayload>? InboxMessage { get; set; } }

public interface IKafkaMessageProp<TKey, TValue> { Message<TKey, TValue>? KafkaMessage { get; set; } }

public interface ITopicPartitionOffsetProp { TopicPartitionOffset? TopicPartitionOffset { get; set; } }
