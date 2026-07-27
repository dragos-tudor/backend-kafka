
namespace Kafka;

partial class KafkaFuncs
{
  [LoggerMessage(1, LogLevel.Error, "Consuming kafka message failed.")]
  static partial void LogConsumeKafkaMessageFailed(ILogger logger, Exception exception);

  [LoggerMessage(2, LogLevel.Error, "Saving inbox message failed. Message id {MessageId}; topic {TopicPartitionOffset}")]
  static partial void LogSaveInboxMessageFailed(ILogger logger, Exception exception, Guid? messageId, TopicPartitionOffset? topicPartitionOffset);

  [LoggerMessage(3, LogLevel.Error, "Applying consumer offset failed. Message id {MessageId}; topic {TopicPartitionOffset}")]
  static partial void LogApplyConsumerOffsetFailed(ILogger logger, Exception exception, Guid? messageId, TopicPartitionOffset topicPartitionOffset);

  [LoggerMessage(4, LogLevel.Error, "Handling inbox message failed. Message id {MessageId}; topic {TopicPartitionOffset}")]
  static partial void LogHandleInboxMessageFailed(ILogger logger, Exception exception, Guid messageId, TopicPartitionOffset topicPartitionOffset);

  [LoggerMessage(5, LogLevel.Error, "Creating Kafka clients failed.")]
  static partial void LogCreateKafkaClientsFailed(ILogger logger, Exception exception);
}