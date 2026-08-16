
namespace Kafka.Messages;

public interface IInboxMessageHandler<TKey, TPayload>
{
  Task<Result<object?, string?>> HandleInboxMessageAsync(InboxMessage<TKey, TPayload> message, CancellationToken ct = default);
}

public interface IInboxPayloadMapper<TValue, TPayload> { TPayload ToInboxPayload(TValue value); }

public interface IInboxPayloadValidator<TPayload> { string? ValidateInboxPayload(TPayload? payload); }

public interface IInsertInboxMessageService<TKey, TPayload>
{
  Task<bool> InsertInboxMessageAsync(InboxMessage<TKey, TPayload> message, CancellationToken ct = default);
}

public interface IReadInboxMessagesService<TKey, TPayload>
{
  Task<IReadOnlyList<InboxMessage<TKey, TPayload>>> GetInboxMessagesAsync(
    DateTime dueAt,
    int batchSize,
    CancellationToken ct = default);
}

public interface IUpdateInboxMessageService<TKey, TPayload>
{
  Task UpdateInboxMessageAsync<TMessage>(
    TMessage message,
    Func<TMessage, TMessage> update,
    CancellationToken ct = default) where TMessage : InboxMessage<TKey, TPayload>;
}

public interface IUpdateInboxMessageSessionService<TKey, TPayload, TSession>
  where TSession: IDisposable
{
  Task UpdateInboxMessageAsync<TMessage>(
    TSession session,
    TMessage message,
    Func<TMessage, TMessage> update,
    CancellationToken ct = default) where TMessage : InboxMessage<TKey, TPayload>;
}