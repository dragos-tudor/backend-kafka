using static Kafka.Operations.Outbox.ValidatingCounters;

namespace Kafka.Operations.Outbox;

partial class OutboxFuncs
{
  [LoggerMessage(21, LogLevel.Debug, "Validated outbox message. MessageId: {messageId}.")]
  static partial void LogValidatedOutboxMessage(ILogger logger, Guid? messageId);

  [LoggerMessage(22, LogLevel.Error, "Validate outbox message error. MessageId: {messageId}.")]
  static partial void LogValidateOutboxMessageError(ILogger logger, Guid? messageId, Exception ex);

  [LoggerMessage(23, LogLevel.Error, "Validate outbox message data error. MessageId: {messageId}. Error: {error}.")]
  static partial void LogValidateOutboxMessageDataError(ILogger logger, Guid? messageId, string error);

  [LoggerMessage(24, LogLevel.Error, "Validate outbox message payload error. MessageId: {messageId}. Error: {error}.")]
  static partial void LogValidateOutboxMessagePayloadError(ILogger logger, Guid? messageId, string error);

  static void InstrumentValidatedOutboxMessage(
    Guid? messageId,
    IInstrumentationServices services)
  {
    LogValidatedOutboxMessage(services.GetLogger(), messageId);
    AddMetricCounter(ValidatedCounter);
    AddActivityEvent(Activity.Current, "validated.outbox.message");
  }

  static void InstrumentValidateOutboxMessageError(
    Guid? messageId,
    Exception ex,
    IInstrumentationServices services)
  {
    LogValidateOutboxMessageError(services.GetLogger(), messageId, ex);
    AddMetricCounter(ValidateErrorCounter);
    AddActivityEvent(Activity.Current, "validate.outbox.message.error");
  }

  static void InstrumentValidateOutboxMessageDataError(
    Guid? messageId,
    string error,
    IInstrumentationServices services)
  {
    LogValidateOutboxMessageDataError(services.GetLogger(), messageId, error);
    AddMetricCounter(ValidateDataErrorCounter);
    AddActivityEvent(Activity.Current, "validate.outbox.message.data.error");
  }

  static void InstrumentValidateOutboxMessagePayloadError(
    Guid? messageId,
    string error,
    IInstrumentationServices services)
  {
    LogValidateOutboxMessagePayloadError(services.GetLogger(), messageId, error);
    AddMetricCounter(ValidatePayloadErrorCounter);
    AddActivityEvent(Activity.Current, "validate.outbox.message.payload.error");
  }
}