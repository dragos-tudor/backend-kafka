using static Kafka.StateMachines.RelayingCounterType;

namespace Kafka.StateMachines;

partial class StateMachinesFuncs
{
  [LoggerMessage(32, LogLevel.Information, "Relayed outbox message. MessageId: {messageId}. State: {state}")]
  static partial void LogRelayedOutboxMessage(ILogger logger, Guid? messageId, string state);

  [LoggerMessage(33, LogLevel.Error, "Relay outbox messages critical error. State: {state}")]
  static partial void LogRelayOutboxMessagesCriticalError(ILogger logger, string state);

  [LoggerMessage(34, LogLevel.Error, "Fetch outbox messages error.")]
  static partial void LogFetchOutboxMessagesError(ILogger logger, Exception exception);

  internal static void InstrumentRelayedOutboxMessage(
    Guid? messageId,
    string state,
    IInstrumentationServices services)
  {
    LogRelayedOutboxMessage(services.GetLogger(), messageId, state);
    AddMetricCounter(services.GetMetricCounters<RelayingCounterType>(), RelayedCounter);
  }

  static void InstrumentRelayOutboxMessageCriticalError(
    string state,
    IInstrumentationServices services)
  {
    LogRelayOutboxMessagesCriticalError(services.GetLogger(), state);
    AddMetricCounter(services.GetMetricCounters<RelayingCounterType>(), RelayCriticalErrorsCounter);
  }

  internal static void InstrumentFetchOutboxMessageError(
    Exception exception,
    IInstrumentationServices services)
  {
    LogFetchOutboxMessagesError(services.GetLogger(), exception);
    AddMetricCounter(services.GetMetricCounters<RelayingCounterType>(), FetchErrorCounter);
  }
}
