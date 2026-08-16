using static Kafka.Operations.Outbox.MappingCounters;

namespace Kafka.Operations.Outbox;

partial class OutboxFuncs
{
  [LoggerMessage(11, LogLevel.Debug, "Mapped outbox message. MessageKey: {messageKey}. MessageId: {messageId}. CorrelationId: {correlationId}.")]
  static partial void LogMappedOutboxMessage(ILogger logger, string? messageKey, Guid? messageId, Guid? correlationId);

  [LoggerMessage(12, LogLevel.Error, "Map outbox message error. MessageId: {messageId}. CorrelationId: {correlationId}.")]
  static partial void LogMapOutboxMessageError(ILogger logger, Guid? messageId, Guid? correlationId, Exception ex);

  [LoggerMessage(13, LogLevel.Error, "Map outbox message payload error. MessageId: {messageId}. CorrelationId: {correlationId}.")]
  static partial void LogMapOutboxMessagePayloadError(ILogger logger, Guid? messageId, Guid? correlationId, Exception ex);

  static void InstrumentMappedOutboxMessage(
    string? messageKey,
    Guid? messageId,
    Guid? correlationId,
    IInstrumentationServices services)
  {
    LogMappedOutboxMessage(services.GetLogger(), messageKey, messageId, correlationId);
    AddActivityTag(Activity.Current, "message.key", messageKey);
    AddActivityTag(Activity.Current, "message.id", messageId);
    AddActivityTag(Activity.Current, "correlation.id", correlationId);
    AddMetricCounter(MappedCounter);
    AddActivityEvent(Activity.Current, "mapped.outbox.message");
  }

  static void InstrumentMapOutboxMessageError(
    Guid? messageId,
    Guid? correlationId,
    Exception ex,
    IInstrumentationServices services)
  {
    LogMapOutboxMessageError(services.GetLogger(), messageId, correlationId, ex);
    AddActivityTag(Activity.Current, "message.id", messageId);
    AddActivityTag(Activity.Current, "correlation.id", correlationId);
    AddMetricCounter(MapErrorCounter);
    AddActivityEvent(Activity.Current, "map.outbox.message.error");
  }

  static void InstrumentMapOutboxMessagePayloadError(
    Guid? messageId,
    Guid? correlationId,
    Exception ex,
    IInstrumentationServices services)
  {
    LogMapOutboxMessagePayloadError(services.GetLogger(), messageId, correlationId, ex);
    AddActivityTag(Activity.Current, "message.id", messageId);
    AddActivityTag(Activity.Current, "correlation.id", correlationId);
    AddMetricCounter(MapPayloadErrorCounter);
    AddActivityEvent(Activity.Current, "map.outbox.message.payload.error");
  }
}