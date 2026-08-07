
namespace Kafka.StateMachines;

partial class StateMachinesFuncs
{
  [LoggerMessage(10, LogLevel.Information, "Resumed inbox message. State: {state}")]
  static partial void LogResumedInboxMessage(ILogger logger, string state);

  [LoggerMessage(11, LogLevel.Error, "Resume inbox message error. State: {state}.")]
  static partial void LogResumeInboxMessageError(ILogger logger, Exception exception, string state);

  [LoggerMessage(12, LogLevel.Error, "Fetch inbox messages error.")]
  static partial void LogFetchInboxMessagesError(ILogger logger, Exception exception);
}
