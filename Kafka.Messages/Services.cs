
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

public interface IGetOutboxMessagesService<TKey, TPayload>
{
  Task<IReadOnlyList<OutboxMessage<TKey, TPayload>>> GetOutboxMessagesAsync(
    DateTime dueAt,
    int batchSize,
    CancellationToken ct = default);
}

public interface IOutboxTopicService<TKey, TPayload> { string GetOutboxTopic(IntegrationMessage<TKey, TPayload> message); }

public interface IUpdateIntegrationMessageService<TKey, TPayload>
{
  Task UpdateIntegrationMessageAsync<TMessage>(
    TMessage message,
    Func<TMessage, TMessage> update,
    CancellationToken ct = default) where TMessage : IntegrationMessage<TKey, TPayload>;
}

public interface IUpdateIntegrationMessageSessionService<TKey, TPayload, TSession> where TSession: IDisposable
{
  Task UpdateIntegrationMessageAsync<TMessage>(
    TSession session,
    TMessage message,
    Func<TMessage, TMessage> update,
    CancellationToken ct = default) where TMessage : IntegrationMessage<TKey, TPayload>;
}
