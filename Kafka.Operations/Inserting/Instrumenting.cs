using static Kafka.Operations.MetricCounterType;

namespace Kafka.Operations;

partial class OperationsFuncs
{
  [LoggerMessage(21, LogLevel.Debug, "Inserted inbox message.")]
  static partial void LogInsertedInboxMessage(ILogger logger);

  [LoggerMessage(22, LogLevel.Debug, "Idempotent inbox message.")]
  static partial void LogIdempotentInboxMessage(ILogger logger);

  static void InstrumentInsertInboxMessage(
    IInstrumentationServices services)
  {
    LogInsertedInboxMessage(services.GetLogger());
    AddMetricCounter(services.GetMetricCounters(), InsertedCounter);
    AddActivityEvent(Activity.Current, "inbox.message.inserted");
  }

  static void InstrumentIdempotentInboxMessage(
    IInstrumentationServices services)
  {
    LogIdempotentInboxMessage(services.GetLogger());
    AddMetricCounter(services.GetMetricCounters(), IdempotentCounter);
    AddActivityEvent(Activity.Current, "inbox.message.idempotent");
  }
}