
namespace Kafka.Messages;

public interface IDeadLetterMessageTopicService { string GetDeadLetterTopic(); }

public interface IInsertDeadLetterMessageService<TKey, TPayload>
{
  Task<bool> InsertDeadLetterMessageAsync(DeadLetterMessage<TKey, TPayload> message, CancellationToken ct = default);
}

public interface IReadDeadLetterMessagesService<TKey, TPayload>
{
  Task<IReadOnlyList<DeadLetterMessage<TKey, TPayload>>> GetDeadLetterMessagesAsync(
    DateTime dueAt,
    int batchSize,
    CancellationToken ct = default);
}

public interface IUpdateDeadLetterMessageService<TKey, TPayload>
{
  Task UpdateDeadLetterAsync<TMessage>(
    TMessage message,
    Func<TMessage, TMessage> update,
    CancellationToken ct = default) where TMessage : DeadLetterMessage<TKey, TPayload>;
}