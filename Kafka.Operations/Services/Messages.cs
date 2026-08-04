
namespace Kafka.Operations;

public interface IHandleInboxMessageService<TKey, TPayload>
{
  Task<Result<object?, string?>> HandleInboxMessageAsync(InboxMessage<TKey, TPayload> message, CancellationToken ct = default);
}

public interface IInboxMessageMapperService<TValue, TPayload> { TPayload ToInboxMessagePayload(TValue value); }

public interface IDeadLetterTopicService<TKey, TPayload> { string GetDeadLetterTopic(InboxMessage<TKey, TPayload> message); }

public interface IKafkaMessageMapperService<TPayload, TValue> { TValue ToKafkaMessageValue(TPayload value); }

