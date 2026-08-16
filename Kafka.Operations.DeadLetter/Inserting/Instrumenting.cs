using static Kafka.Operations.DeadLetter.InsertingCounters;

namespace Kafka.Operations.DeadLetter;

partial class DeadLetterFuncs
{
  [LoggerMessage(7, LogLevel.Debug, "Inserted dead letter message. MessageId: {messageId}")]
  static partial void LogInsertedDeadLetterMessage(ILogger logger, Guid? messageId);

  [LoggerMessage(8, LogLevel.Error, "Insert dead letter message error. MessageId: {messageId}")]
  static partial void LogInsertDeadLetterMessageError(ILogger logger, Guid? messageId, Exception ex);

  [LoggerMessage(9, LogLevel.Debug, "Idempotent dead letter message. MessageId: {messageId}")]
  static partial void LogIdempotentDeadLetterMessage(ILogger logger, Guid? messageId);

  static void InstrumentInsertedDeadLetterMessage(
    Guid? messageId,
    IInstrumentationServices services)
  {
    LogInsertedDeadLetterMessage(services.GetLogger(), messageId);
    AddMetricCounter(InsertedCounter);
    AddActivityEvent(Activity.Current, "inserted.dead.letter");
  }

  static void InstrumentInsertDeadLetterMessageError(
    Guid? messageId,
    Exception ex,
    IInstrumentationServices services)
  {
    LogInsertDeadLetterMessageError(services.GetLogger(), messageId, ex);
    AddMetricCounter(InsertErrorCounter);
    AddActivityEvent(Activity.Current, "insert.dead.letter.message.error");
  }

  static void InstrumentIdempotentDeadLetterMessage(
    Guid? messageId,
    IInstrumentationServices services)
  {
    LogIdempotentDeadLetterMessage(services.GetLogger(), messageId);
    AddActivityEvent(Activity.Current, "idempotent.dead.letter.message");
  }
}