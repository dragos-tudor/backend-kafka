using static Kafka.Operations.Inbox.InsertingCounters;

namespace Kafka.Operations.Inbox;

partial class InboxFuncs
{
  [LoggerMessage(7, LogLevel.Debug, "Inserted inbox message. MessageId: {messageId}")]
  static partial void LogInsertedInboxMessage(ILogger logger, Guid? messageId);

  [LoggerMessage(8, LogLevel.Error, "Insert inbox message error. MessageId: {messageId}")]
  static partial void LogInsertInboxMessageError(ILogger logger, Guid? messageId, Exception ex);

  [LoggerMessage(9, LogLevel.Debug, "Idempotent inbox message. MessageId: {messageId}")]
  static partial void LogIdempotentInboxMessage(ILogger logger, Guid? messageId);

  static void InstrumentInsertedInboxMessage(
    Guid? messageId,
    IInstrumentationServices services)
  {
    LogInsertedInboxMessage(services.GetLogger(), messageId);
    AddMetricCounter(InsertedCounter);
    AddActivityEvent(Activity.Current, "inserted.inbox.message");
  }

  static void InstrumentInsertInboxMessageError(
    Guid? messageId,
    Exception ex,
    IInstrumentationServices services)
  {
    LogInsertInboxMessageError(services.GetLogger(), messageId, ex);
    AddMetricCounter(InsertErrorCounter);
    AddActivityEvent(Activity.Current, "insert.inbox.message.error");
  }

  static void InstrumentIdempotentInboxMessage(
    Guid? messageId,
    IInstrumentationServices services)
  {
    LogIdempotentInboxMessage(services.GetLogger(), messageId);
    AddActivityEvent(Activity.Current, "idempotent.inbox.message");
  }
}