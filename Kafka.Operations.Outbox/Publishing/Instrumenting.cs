using static Kafka.Operations.Outbox.PublishingCounterType;

namespace Kafka.Operations.Outbox;

partial class OutboxFuncs
{
  [LoggerMessage(18, LogLevel.Information, "Published outbox message. MessageId: {messageId}. MessageKey: {messageKey}. MessageTopic: {messageTopic}. Handle error: {handleError}")]
  static partial void LogPublishedOutboxMessage(ILogger logger, Guid? messageId, string? messageKey, string messageTopic, string? handleError);

  [LoggerMessage(19, LogLevel.Error, "Publish outbox message error. MessageId: {messageId}.")]
  static partial void LogPublishOutboxMessageError(ILogger logger, Guid? messageId, Exception exception);

  static void InstrumentPublishedOutboxMessage(
    Guid? messageId,
    string? messageKey,
    string messageTopic,
    IInstrumentationServices services)
  {
    LogPublishedOutboxMessage(services.GetLogger(), messageId, messageKey, messageTopic, null);
    AddMetricCounter(services.GetMetricCounters<PublishingCounterType>(), PublishedOutboxCounter);
    AddActivityTag(Activity.Current, "publish.outbox.key", messageKey);
    AddActivityTag(Activity.Current, "publish.outbox.topic", messageTopic);
    AddActivityEvent(Activity.Current, "published.outbox");
  }

  static void InstrumentPublishOutboxMessageError(
    Guid? messageId,
    Exception ex,
    IInstrumentationServices services)
  {
    LogPublishOutboxMessageError(services.GetLogger(), messageId, ex);
    AddMetricCounter(services.GetMetricCounters<PublishingCounterType>(), PublishOutboxErrorCounter);
    AddActivityEvent(Activity.Current, "publish.outbox.error", [
      CreateActivityEventAttribute("publish.error", ex),
    ]);
  }
}