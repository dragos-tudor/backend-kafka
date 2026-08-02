
namespace Kafka.Engine;

public interface IGetDeadLetterTopicService<TKey, TPayload> { string GetDeadLetterTopic(InboxMessage<TKey, TPayload> message); }

public interface IHandleInboxMessage<TKey, TPayload>
{
  Task<Result<object?, string?>> HandleInboxMessageAsync(InboxMessage<TKey, TPayload> message, CancellationToken ct = default);
}

public interface IKafkaMessageMapperService<TPayload, TValue> { TValue ToKafkaMessageValue(TPayload value); }

public interface IPersistedMessageMapper<TValue, TPayload> { TPayload ToPersistedMessagePayload(TValue value); }

