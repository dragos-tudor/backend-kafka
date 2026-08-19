using static Kafka.Operations.Inbox.MappingCounters;

namespace Kafka.Operations.Inbox;

partial class InboxFuncs
{
  [LoggerMessage(11, LogLevel.Debug, "Mapped kafka message. MessageKey: {messageKey}. MessageId: {messageId}. CorrelationId: {correlationId}.")]
  static partial void LogMappedKafkaMessage(ILogger logger, string? messageKey, Guid? messageId, Guid? correlationId);

  [LoggerMessage(12, LogLevel.Error, "Map kafka message error. MessageKey: {messageKey}.")]
  static partial void LogMapKafkaMessageError(ILogger logger, string? messageKey, Exception ex);

  [LoggerMessage(13, LogLevel.Error, "Map kafka message value error. MessageKey: {messageKey}.")]
  static partial void LogMapKafkaMessageValueError(ILogger logger, string? messageKey, Exception ex);

  static void InstrumentMappedKafkaMessage(
    string? messageKey,
    Guid? messageId,
    Guid? correlationId,
    IInstrumentationServices services)
  {
    LogMappedKafkaMessage(services.GetLogger(), messageKey, messageId, correlationId);
    AddActivityTag(Activity.Current, "message.id", messageId);
    AddActivityTag(Activity.Current, "correlation.id", correlationId);
    AddActivityTag(Activity.Current, "message.key", messageKey);
    AddMetricCounter(MappedCounter);
    AddActivityEvent(Activity.Current, "mapped.kafka.message");
  }

  static void InstrumentMapKafkaMessageError(
    string? messageKey,
    Exception ex,
    IInstrumentationServices services)
  {
    LogMapKafkaMessageError(services.GetLogger(), messageKey, ex);
    AddMetricCounter(MapErrorCounter);
    AddActivityEvent(Activity.Current, "map.kafka.message.error");
  }

  static void InstrumentMapKafkaMessageValueError(
    string? messageKey,
    Exception ex,
    IInstrumentationServices services)
  {
    LogMapKafkaMessageValueError(services.GetLogger(), messageKey, ex);
    AddActivityTag(Activity.Current, "message.key", messageKey);
    AddMetricCounter(MapValueErrorCounter);
    AddActivityEvent(Activity.Current, "map.kafka.message.value.error");
  }
}