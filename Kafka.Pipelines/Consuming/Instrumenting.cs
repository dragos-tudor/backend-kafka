using static Kafka.Pipelines.ConsumingCounters;

namespace Kafka.Pipelines;

partial class PipelinesFuncs
{
  [LoggerMessage(30, LogLevel.Information, "Consumed kafka message. State: {state}")]
  static partial void LogConsumedKafkaMessage(ILogger logger, string? state);

  [LoggerMessage(31, LogLevel.Error, "Consuming kafka message critical error. State: {state}.")]
  static partial void LogConsumeKafkaMessageCriticalError(ILogger logger, string? state);

  static void InstrumentConsumeKafkaMessage(
    string? state,
    IInstrumentationServices services)
  {
    LogConsumedKafkaMessage(services.GetLogger(), state);
    AddMetricCounter(ConsumedCounter);
  }

  static void InstrumentConsumeKafkaMessageCriticalError(
    string? state,
    IInstrumentationServices services)
  {
    LogConsumeKafkaMessageCriticalError(services.GetLogger(), state);
    AddMetricCounter(ConsumeCriticalErrorsCounter);
  }
}