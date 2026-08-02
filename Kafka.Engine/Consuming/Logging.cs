
namespace Kafka.Engine;

partial class EngineFuncs
{
  [LoggerMessage(1, LogLevel.Information, "Captured kafka message. MessageId: {messageId}, Offset: {offset}. CorrelationId: {correlationId}.")]
  static partial void LogCapturedKafkaMessage(ILogger logger, Guid? messageId, TopicPartitionOffset offset, Guid? correlationId);

  [LoggerMessage(2, LogLevel.Debug, "Inserted inbox message. MessageId: {messageId}, Offset: {offset}. CorrelationId: {correlationId}.")]
  static partial void LogInsertedInboxMessage(ILogger logger, Guid? messageId, TopicPartitionOffset offset, Guid? correlationId);

  [LoggerMessage(4, LogLevel.Debug, "Applied consumer offset. MessageId: {messageId}, Applied offset: {offset}. CorrelationId: {correlationId}.")]
  static partial void LogAppliedConsumerOffset(ILogger logger, Guid? messageId, TopicPartitionOffset? offset, Guid? correlationId);

  [LoggerMessage(5, LogLevel.Debug, "Handled inbox message. MessageId: {messageId}, Offset: {offset}. Error: {error}. CorrelationId: {correlationId}.")]
  static partial void LogHandledInboxMessage(ILogger logger, Guid? messageId, TopicPartitionOffset offset, string? error, Guid? correlationId);

  [LoggerMessage(5, LogLevel.Debug, "Published dead letter. MessageId: {messageId}, Offset: {offset}. CorrelationId: {correlationId}.")]
  static partial void LogPublishedDeadLetter(ILogger logger, Guid? messageId, TopicPartitionOffset offset, Guid? correlationId);

  [LoggerMessage(6, LogLevel.Information, "Consumed kafka message. State: {state}")]
  static partial void LogConsumedKafkaMessage(ILogger logger, ConsumingState state);

  [LoggerMessage(6, LogLevel.Error, "Consuming kafka message failed. State: {state}.")]
  static partial void LogConsumeKafkaMessageFailed(ILogger logger, Exception exception, ConsumingState state);

  internal static IDisposable CreateLogScopeForMessage(
    ILogger logger,
    Activity? activity,
    TopicPartitionOffset offset,
    Guid? messageId,
    Guid? correlationId)
  {
    var scope = new Dictionary<string, object?>
    {
      ["traceId"] = activity?.TraceId.ToString(),
      ["spanId"] = activity?.SpanId.ToString(),
      ["messageId"] = messageId.ToString(),
      ["correlationId"] = correlationId?.ToString(),
      ["topic"] = offset.Topic,
      ["partition"] = offset.Partition.ToString(),
      ["offset"] = offset.Offset.ToString()
    };
    return logger.BeginScope(scope)!;
  }
}