using static Kafka.Operations.DeadLetter.MappingCounters;

namespace Kafka.Operations.DeadLetter;

partial class DeadLetterFuncs
{
  [LoggerMessage(11, LogLevel.Debug, "Mapped dead letter message. MessageKey: {messageKey}. MessageId: {messageId}. CorrelationId: {correlationId}.")]
  static partial void LogMappedDeadLetterMessage(ILogger logger, string? messageKey, Guid? messageId, Guid? correlationId);

  [LoggerMessage(12, LogLevel.Error, "Map dead letter message error. MessageId: {messageId}. CorrelationId: {correlationId}.")]
  static partial void LogMapDeadLetterMessageError(ILogger logger, Guid? messageId, Guid? correlationId, Exception ex);

  [LoggerMessage(13, LogLevel.Error, "Map dead letter message payload error. MessageId: {messageId}. CorrelationId: {correlationId}.")]
  static partial void LogMapDeadLetterMessagePayloadError(ILogger logger, Guid? messageId, Guid? correlationId, Exception ex);

  static void InstrumentMappedDeadLetterMessage(
    string? messageKey,
    Guid? messageId,
    Guid? correlationId,
    IInstrumentationServices services)
  {
    LogMappedDeadLetterMessage(services.GetLogger(), messageKey, messageId, correlationId);
    AddActivityTag(Activity.Current, "message.key", messageKey);
    AddActivityTag(Activity.Current, "message.id", messageId);
    AddActivityTag(Activity.Current, "correlation.id", correlationId);
    AddMetricCounter(MappedCounter);
    AddActivityEvent(Activity.Current, "mapped.dead letter.message");
  }

  static void InstrumentMapDeadLetterMessageError(
    Guid? messageId,
    Guid? correlationId,
    Exception ex,
    IInstrumentationServices services)
  {
    LogMapDeadLetterMessageError(services.GetLogger(), messageId, correlationId, ex);
    AddActivityTag(Activity.Current, "message.id", messageId);
    AddActivityTag(Activity.Current, "correlation.id", correlationId);
    AddMetricCounter(MapErrorCounter);
    AddActivityEvent(Activity.Current, "map.dead letter.message.error");
  }

  static void InstrumentMapDeadLetterMessagePayloadError(
    Guid? messageId,
    Guid? correlationId,
    Exception ex,
    IInstrumentationServices services)
  {
    LogMapDeadLetterMessagePayloadError(services.GetLogger(), messageId, correlationId, ex);
    AddActivityTag(Activity.Current, "message.id", messageId);
    AddActivityTag(Activity.Current, "correlation.id", correlationId);
    AddMetricCounter(MapPayloadErrorCounter);
    AddActivityEvent(Activity.Current, "map.dead letter.message.payload.error");
  }
}