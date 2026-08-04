
namespace Kafka.StateMachines;

partial class StateMachinesFuncs
{
  [LoggerMessage(8, LogLevel.Information, "Consumed kafka message. State: {state}")]
  static partial void LogConsumedKafkaMessage(ILogger logger, OperationState state);

  [LoggerMessage(9, LogLevel.Error, "Consuming kafka message failed. State: {state}.")]
  static partial void LogConsumingKafkaMessageFailed(ILogger logger, Exception exception, OperationState state);
}