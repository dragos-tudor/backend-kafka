
namespace Kafka.Engine;

partial class EngineFuncs
{
  [LoggerMessage(1, LogLevel.Information, "Captured kafka message. MessageId: {messageId}, Offset: {offset}.")]
  static partial void LogCapturedKafkaMessage(ILogger logger, Guid? messageId, TopicPartitionOffset offset);

  [LoggerMessage(2, LogLevel.Information, "Inserted inbox message. MessageId: {messageId}, Offset: {offset}.")]
  static partial void LogInsertedInboxMessage(ILogger logger, Guid? messageId, TopicPartitionOffset offset);

  [LoggerMessage(4, LogLevel.Information, "Applied consumer offset. MessageId: {messageId}, Applied offset: {offset}.")]
  static partial void LogAppliedConsumerOffset(ILogger logger, Guid? messageId, TopicPartitionOffset? offset);

  [LoggerMessage(5, LogLevel.Information, "Handled inbox message. MessageId: {messageId}, Offset: {offset}. Error: {error}.")]
  static partial void LogHandledInboxMessage(ILogger logger, Guid? messageId, TopicPartitionOffset offset, string? error);

  [LoggerMessage(5, LogLevel.Information, "Published dead letter. MessageId: {messageId}, Offset: {offset}.")]
  static partial void LogPublishedDeadLetter(ILogger logger, Guid? messageId, TopicPartitionOffset offset);

  [LoggerMessage(6, LogLevel.Error, "Consuming kafka message failed. State: {state}.")]
  static partial void LogConsumeKafkaMessageFailed(ILogger logger, Exception exception, ConsumingState state);
}