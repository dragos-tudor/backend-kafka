
namespace Kafka.Operations;

public interface IDomainErrorProp { string? DomainError { get; set; } }

public interface IKafkaMessageProp<TKey, TValue> { Message<TKey, TValue>? KafkaMessage { get; set; } }

public interface IInboxMessageProp<TKey, TPayload> { InboxMessage<TKey, TPayload>? InboxMessage { get; set; } }

public interface IOffsetAppliedProp { bool? OffsetApplied { get; set; } }

public interface ITopicPartitionOffsetProp { TopicPartitionOffset? TopicPartitionOffset { get; set; } }
