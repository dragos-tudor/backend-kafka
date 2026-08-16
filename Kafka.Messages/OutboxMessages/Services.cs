
namespace Kafka.Messages;

public interface IInsertOutboxMessageSessionService<TKey, TPayload, TSession>
{
  Task<bool> InsertOutboxMessageAsync(TSession session, OutboxMessage<TKey, TPayload> message, CancellationToken ct = default);
}

public interface IOutboxMessageTopicService<TKey, TPayload> { string GetOutboxTopic(OutboxMessage<TKey, TPayload> message); }

public interface IOutboxPayloadValidator<TPayload> { string? ValidateOutboxPayload(TPayload? payload); }

public interface IReadOutboxMessagesService<TKey, TPayload>
{
  Task<IReadOnlyList<OutboxMessage<TKey, TPayload>>> GetOutboxMessagesAsync(
    DateTime dueAt,
    int batchSize,
    CancellationToken ct = default);
}

public interface IUpdateOutboxMessageService<TKey, TPayload>
{
  Task UpdateOutboxMessageAsync<TMessage>(
    TMessage message,
    Func<TMessage, TMessage> update,
    CancellationToken ct = default) where TMessage : OutboxMessage<TKey, TPayload>;
}