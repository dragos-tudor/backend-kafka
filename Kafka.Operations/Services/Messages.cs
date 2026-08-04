
namespace Kafka.Operations;

public interface IHandleInboxMessageService<TKey, TPayload>
{
  Task<Result<object?, string?>> HandleInboxMessageAsync(InboxMessage<TKey, TPayload> message, CancellationToken ct = default);
}

public interface IInboxMessageMapperService<TValue, TPayload> { TPayload ToInboxMessagePayload(TValue value); }

public interface IKafkaDeadLetterTopicService<TKey, TPayload> { string GetKafkaDeadLetterTopic(InboxMessage<TKey, TPayload> message); }

public interface IKafkaMessageMapperService<TPayload, TValue> { TValue ToKafkaMessageValue(TPayload value); }

