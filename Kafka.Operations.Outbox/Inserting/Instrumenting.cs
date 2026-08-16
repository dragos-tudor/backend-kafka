using static Kafka.Operations.Outbox.InsertingCounters;

namespace Kafka.Operations.Outbox;

partial class OutboxFuncs
{
  [LoggerMessage(7, LogLevel.Debug, "Inserted outbox message. MessageId: {messageId}. Status: {status}")]
  static partial void LogInsertedOutboxMessage(ILogger logger, Guid? messageId, OutboxMessageStatus status);

  [LoggerMessage(8, LogLevel.Error, "Insert outbox message error. MessageId: {messageId}")]
  static partial void LogInsertOutboxMessageError(ILogger logger, Guid? messageId, Exception ex);

  [LoggerMessage(9, LogLevel.Debug, "Idempotent outbox message. MessageId: {messageId}")]
  static partial void LogIdempotentOutboxMessage(ILogger logger, Guid? messageId);

  static void InstrumentInsertedOutboxMessage(
    Guid? messageId,
    OutboxMessageStatus status,
    IInstrumentationServices services)
  {
    LogInsertedOutboxMessage(services.GetLogger(), messageId, status);
    AddMetricCounter(InsertedCounter);
    AddActivityEvent(Activity.Current, "inserted.message");
  }

  static void InstrumentInsertOutboxMessageError(
    Guid? messageId,
    Exception ex,
    IInstrumentationServices services)
  {
    LogInsertOutboxMessageError(services.GetLogger(), messageId, ex);
    AddMetricCounter(InsertErrorCounter);
    AddActivityEvent(Activity.Current, "insert.message.error");
  }

  static void InstrumentIdempotentOutboxMessage(
    Guid? messageId,
    IInstrumentationServices services)
  {
    LogIdempotentOutboxMessage(services.GetLogger(), messageId);
    AddActivityEvent(Activity.Current, "idempotent.message");
  }
}