using static Kafka.Operations.DeadLetter.ConvertingCounters;

namespace Kafka.Operations.DeadLetter;

partial class DeadLetterFuncs
{
  [LoggerMessage(11, LogLevel.Debug, "Converted deadletter message. MessageId: {messageId}. CorrelationId: {correlationId}.")]
  static partial void LogConvertedDeadLetterMessage(ILogger logger, Guid? messageId, Guid? correlationId);

  [LoggerMessage(12, LogLevel.Error, "Convert deadletter message error. MessageId: {messageId}. CorrelationId: {correlationId}.")]
  static partial void LogConvertDeadLetterMessageError(ILogger logger, Guid? messageId, Guid? correlationId, Exception ex);

  static void InstrumentConvertedDeadLetterMessage(
    Guid? messageId,
    Guid? correlationId,
    IInstrumentationServices services)
  {
    LogConvertedDeadLetterMessage(services.GetLogger(), messageId, correlationId);
    AddActivityTag(Activity.Current, "message.id", messageId);
    AddActivityTag(Activity.Current, "correlation.id", correlationId);
    AddMetricCounter(ConvertedCounter);
    AddActivityEvent(Activity.Current, "converted.deadletter.message");
  }

  static void InstrumentConvertDeadLetterMessageError(
    Guid? messageId,
    Guid? correlationId,
    Exception ex,
    IInstrumentationServices services)
  {
    LogConvertDeadLetterMessageError(services.GetLogger(), messageId, correlationId, ex);
    AddActivityTag(Activity.Current, "message.id", messageId);
    AddActivityTag(Activity.Current, "correlation.id", correlationId);
    AddMetricCounter(ConvertErrorCounter);
    AddActivityEvent(Activity.Current, "convert.deadletter.message.error");
  }
}