
namespace Kafka.Engine;

public interface IAppliedOffsetData { long? AppliedOffset { get; set; } }

public interface ICorrelationIdData { Guid? CorrelationId { get; set; } }

public interface IDomainErrorData { string? DomainError { get; set; } }

public interface IKafkaMessageData<TKey, TValue>
{
  Message<TKey, TValue>? KafkaMessage { get; set; }
}

public interface IInboxMessageData<TKey, TPayload>
{
  InboxMessage<TKey, TPayload>? Message { get; set; }
}

public interface IMessageIdData { Guid? MessageId { get; set; } }

public interface ITopicPartitionOffsetData { TopicPartitionOffset? Offset { get; set; } }
