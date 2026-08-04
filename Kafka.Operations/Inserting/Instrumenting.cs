using static Kafka.Operations.MetricCounterType;

namespace Kafka.Operations;

partial class OperationsFuncs
{
  [LoggerMessage(21, LogLevel.Debug, "Inserted inbox message. MessageId: {messageId}")]
  static partial void LogInsertedInboxMessage(ILogger logger, Guid? messageId);

  [LoggerMessage(22, LogLevel.Debug, "Idempotent inbox message. MessageId: {messageId}")]
  static partial void LogIdempotentInboxMessage(ILogger logger, Guid? messageId);

  static void InstrumentInsertInboxMessage(
    Guid? messageId,
    IInstrumentationServices services)
  {
    LogInsertedInboxMessage(services.GetLogger(), messageId);
    AddMetricCounter(services.GetMetricCounters(), InsertedCounter);
    AddActivityEvent(Activity.Current, "inbox.message.inserted");
  }

  static void InstrumentIdempotentInboxMessage(
    Guid? messageId,
    IInstrumentationServices services)
  {
    LogIdempotentInboxMessage(services.GetLogger(), messageId);
    AddMetricCounter(services.GetMetricCounters(), IdempotentCounter);
    AddActivityEvent(Activity.Current, "inbox.message.idempotent");
  }
}