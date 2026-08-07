
namespace Kafka.Messages;

public interface IDeadLetterTopicService<TKey, TPayload> { string GetDeadLetterTopic(IntegrationMessage<TKey, TPayload> message); }

public interface IGetInboxMessagesService<TKey, TPayload>
{
  Task<IReadOnlyList<InboxMessage<TKey, TPayload>>> GetInboxMessagesAsync(
    DateTime dueAt,
    int batchSize,
    CancellationToken ct = default);
}

public interface IInsertInboxMessageService<TKey, TPayload>
{
  Task<bool> InsertInboxMessageAsync(InboxMessage<TKey, TPayload> message, CancellationToken ct = default);
}

public interface IIntegrationPayloadMapperService<TValue, TPayload> { TPayload ToIntegrationPayload(TValue value); }

public interface IKafkaValueMapperService<TPayload, TValue> { TValue ToKafkaValue(TPayload value); }

public interface IUpdateIntegrationMessageService<TKey, TPayload>
{
  Task UpdateIntegrationMessageAsync<TMessage, TState>(
    TMessage message,
    TState state,
    CancellationToken ct = default) where TMessage : IntegrationMessage<TKey, TPayload>;
}

public interface IUpdateIntegrationMessageSessionService<TKey, TPayload, TSession> where TSession: IDisposable
{
  Task UpdateIntegrationMessageAsync<TMessage, TState>(
    TSession session,
    TMessage message,
    TState state,
    CancellationToken ct = default) where TMessage : IntegrationMessage<TKey, TPayload>;
}
