using static Kafka.Pipelines.PublishingCounters;

namespace Kafka.Pipelines;

partial class PipelinesFuncs
{
  [LoggerMessage(32, LogLevel.Information, "Published outbox message. MessageId: {messageId}. State: {state}")]
  static partial void LogPublishedOutboxMessage(ILogger logger, Guid? messageId, string state);

  [LoggerMessage(33, LogLevel.Error, "Publish outbox message critical error. State: {state}")]
  static partial void LogPublishOutboxMessageCriticalError(ILogger logger, string state);

  internal static void InstrumentPublishedOutboxMessage(
    Guid? messageId,
    string state,
    IInstrumentationServices services)
  {
    LogPublishedOutboxMessage(services.GetLogger(), messageId, state);
    AddMetricCounter(PublishedCounter);
  }

  static void InstrumentPublishOutboxMessageCriticalError(
    string state,
    IInstrumentationServices services)
  {
    LogPublishOutboxMessageCriticalError(services.GetLogger(), state);
    AddMetricCounter(PublishCriticalErrorsCounter);
  }
}
