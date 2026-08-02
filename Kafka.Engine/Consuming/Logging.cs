
namespace Kafka.Engine;

partial class EngineFuncs
{
  [LoggerMessage(1, LogLevel.Information, "Captured kafka message.")]
  static partial void LogCapturedKafkaMessage(ILogger logger);

  [LoggerMessage(2, LogLevel.Debug, "Inserted inbox message.")]
  static partial void LogInsertedInboxMessage(ILogger logger);

  [LoggerMessage(4, LogLevel.Debug, "Applied consumer offset.")]
  static partial void LogAppliedConsumerOffset(ILogger logger);

  [LoggerMessage(5, LogLevel.Debug, "Handled inbox message.")]
  static partial void LogHandledInboxMessage(ILogger logger);

  [LoggerMessage(5, LogLevel.Debug, "Published dead letter.")]
  static partial void LogPublishedDeadLetter(ILogger logger);

  [LoggerMessage(6, LogLevel.Information, "Consumed kafka message. State: {state}")]
  static partial void LogConsumedKafkaMessage(ILogger logger, ConsumingState state);

  [LoggerMessage(6, LogLevel.Error, "Consuming kafka message failed. State: {state}.")]
  static partial void LogConsumeKafkaMessageFailed(ILogger logger, Exception exception, ConsumingState state);

  [LoggerMessage(7, LogLevel.Error, "Handled inbox message. Doamin error: {domainError}")]
  static partial void LogHandledInboxMessageFailed(ILogger logger, string domainError);

  internal static IDisposable CreateLogScopeForMessage(
    ILogger logger,
    Activity? activity,
    string component,
    TopicPartitionOffset offset,
    Guid? messageId,
    Guid? correlationId)
  {
    var scope = new Dictionary<string, object?>
    {
      ["traceId"] = activity?.TraceId.ToString(),
      ["spanId"] = activity?.SpanId.ToString(),
      ["component"] = component,
      ["topic"] = offset.Topic,
      ["partition"] = offset.Partition.ToString(),
      ["offset"] = offset.Offset.ToString(),
      ["messageId"] = messageId.ToString(),
      ["correlationId"] = correlationId?.ToString(),
    };
    return logger.BeginScope(scope)!;
  }
}