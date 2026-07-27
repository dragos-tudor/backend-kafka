
namespace Kafka;

public interface IServices<TKey, TValue, TPayload> :
  IProcessKafkaMessagesServices<TKey, TValue, TPayload>,
  IConsumeKafkaMessageServices<TKey, TValue, TPayload>,
  IHandleInboxMessageServices<TKey, TValue, TPayload>,
  ISaveInboxMessageServices<TKey, TValue, TPayload>;

public interface IProcessKafkaMessagesServices<TKey, TValue, TPayload> :
  IConsumeKafkaMessageServices<TKey, TValue, TPayload>;

public interface IConsumeKafkaMessageServices<TKey, TValue, TPayload> :
  IHandleInboxMessageServices<TKey, TValue, TPayload>,
  ISaveInboxMessageServices<TKey, TValue, TPayload>,
  ILoggerService;

public interface IHandleInboxMessageServices<TKey, TValue, TPayload> :
  IHandleInboxMessageService<TKey, TValue, TPayload>,
  IUpdateInboxMessageService<TKey, TValue, TPayload>,
  IDeadLetterTopicService,
  IDateTimeService,
  IMapperServices<TValue, TPayload>;

public interface ISaveInboxMessageServices<TKey, TValue, TPayload> :
  ISaveInboxMessageService<TKey, TValue, TPayload>,
  IMapperServices<TValue, TPayload>;

public interface IDateTimeService { DateTime GetUtcDate(); }

public interface ILoggerService { ILogger GetLogger(string categoryName); }

public interface IMapperServices<TValue, TPayload> { TPayload ToMessagePayload(TValue value); TValue ToMessageValue(TPayload value); }

public interface IDeadLetterTopicService { string GetDeadLetterTopic(string topicName); }

public interface IHandleInboxMessageService<TKey, TValue, TPayload>
{
  Task<string?> HandleInboxMessage(InboxMessage<TKey, TPayload> message, CancellationToken cancellationToken = default);
}

public interface ISaveInboxMessageService<TKey, TValue, TPayload>
{
  Task<bool> SaveInboxMessage(InboxMessage<TKey, TPayload> message, TopicPartitionOffset topicPartitionOffset, CancellationToken cancellationToken = default);
}

public interface IUpdateInboxMessageService<TKey, TValue, TPayload>
{
  Task UpdateInboxMessageStatus(InboxMessage<TKey, TPayload> message, InboxMessageStatus status, CancellationToken cancellationToken = default);
}
