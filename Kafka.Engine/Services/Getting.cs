
namespace Kafka.Engine;

public interface IGetUtcDateService { DateTime GetUtcDate(); }

public interface IGetDeadLetterTopicService<TKey, TPayload> { string GetDeadLetterTopic(InboxMessage<TKey, TPayload> message); }

public interface IGetDeadLetteringInboxMessagesService
{
  Task<IReadOnlyList<InboxMessage<TKey, TPayload>>> GetDeadLetteringInboxMessagesAsync<TKey, TPayload>(DateTimeOffset olderThan, int batchSize, CancellationToken ct);
}

public interface IGetLoggerService { ILogger GetLogger(); }

public interface IGetSessionService<TSession> where TSession : IDisposable { TSession GetSession(); }

public interface IGetPendingInboxMessagesService
{
  Task<IReadOnlyList<InboxMessage<TKey, TPayload>>> GetPendingInboxMessagesAsync<TKey, TPayload>(DateTimeOffset olderThan, int batchSize, CancellationToken ct);
}