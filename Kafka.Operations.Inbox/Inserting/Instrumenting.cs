using static Kafka.Operations.Inbox.InsertingCounterType;

namespace Kafka.Operations.Inbox;

partial class InboxFuncs
{
  [LoggerMessage(4, LogLevel.Debug, "Inserted inbox message. MessageId: {messageId}")]
  static partial void LogInsertedInboxMessage(ILogger logger, Guid? messageId);

  [LoggerMessage(5, LogLevel.Error, "Insert inbox message error. MessageKey: {messageKey}")]
  static partial void LogInsertInboxMessageError(ILogger logger, string? messageKey, Exception ex);

  [LoggerMessage(6, LogLevel.Debug, "Idempotent inbox message. MessageId: {messageId}")]
  static partial void LogIdempotentInboxMessage(ILogger logger, Guid? messageId);

  static void InstrumentInsertedInboxMessage(
    Guid? messageId,
    IInstrumentationServices services)
  {
    LogInsertedInboxMessage(services.GetLogger(), messageId);
    AddMetricCounter(services.GetMetricCounters<InsertingCounterType>(), InsertedCounter);
    AddActivityEvent(Activity.Current, "inserted.message");
  }

  static void InstrumentInsertInboxMessageError(
    string? messageKey,
    Exception ex,
    IInstrumentationServices services)
  {
    LogInsertInboxMessageError(services.GetLogger(), messageKey, ex);
    AddMetricCounter(services.GetMetricCounters<InsertingCounterType>(), InsertErrorCounter);
    AddActivityEvent(Activity.Current, "insert.message.error");
  }

  static void InstrumentIdempotentInboxMessage(
    Guid? messageId,
    IInstrumentationServices services)
  {
    LogIdempotentInboxMessage(services.GetLogger(), messageId);
    AddActivityEvent(Activity.Current, "idempotent.message");
  }
}