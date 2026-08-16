using static Kafka.Operations.Inbox.ValidatingCounters;

namespace Kafka.Operations.Inbox;

partial class InboxFuncs
{
  [LoggerMessage(21, LogLevel.Debug, "Validated inbox message. MessageId: {messageId}.")]
  static partial void LogValidatedInboxMessage(ILogger logger, Guid? messageId);

  [LoggerMessage(22, LogLevel.Error, "Validate inbox message error. MessageId: {messageId}.")]
  static partial void LogValidateInboxMessageError(ILogger logger, Guid? messageId, Exception ex);

  [LoggerMessage(23, LogLevel.Error, "Validate inbox message data error. MessageId: {messageId}. Error: {error}.")]
  static partial void LogValidateInboxMessageDataError(ILogger logger, Guid? messageId, string error);

  [LoggerMessage(24, LogLevel.Error, "Validate inbox message payload error. MessageId: {messageId}. Error: {error}.")]
  static partial void LogValidateInboxMessagePayloadError(ILogger logger, Guid? messageId, string error);

  static void InstrumentValidatedInboxMessage(
    Guid? messageId,
    IInstrumentationServices services)
  {
    LogValidatedInboxMessage(services.GetLogger(), messageId);
    AddMetricCounter(ValidatedCounter);
    AddActivityEvent(Activity.Current, "validated.inbox.message");
  }

  static void InstrumentValidateInboxMessageError(
    Guid? messageId,
    Exception ex,
    IInstrumentationServices services)
  {
    LogValidateInboxMessageError(services.GetLogger(), messageId, ex);
    AddMetricCounter(ValidateErrorCounter);
    AddActivityEvent(Activity.Current, "validate.inbox.message.error");
  }

  static void InstrumentValidateInboxMessageDataError(
    Guid? messageId,
    string error,
    IInstrumentationServices services)
  {
    LogValidateInboxMessageDataError(services.GetLogger(), messageId, error);
    AddMetricCounter(ValidateDataErrorCounter);
    AddActivityEvent(Activity.Current, "validate.inbox.message.data.error");
  }

  static void InstrumentValidateInboxMessagePayloadError(
    Guid? messageId,
    string error,
    IInstrumentationServices services)
  {
    LogValidateInboxMessagePayloadError(services.GetLogger(), messageId, error);
    AddMetricCounter(ValidatePayloadErrorCounter);
    AddActivityEvent(Activity.Current, "validate.inbox.message.payload.error");
  }
}