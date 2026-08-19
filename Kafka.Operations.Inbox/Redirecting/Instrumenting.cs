using static Kafka.Operations.Inbox.RedirectingCounters;

namespace Kafka.Operations.Inbox;

partial class InboxFuncs
{
  [LoggerMessage(92, LogLevel.Information, "Redirected kafka message. MessageKey: {messageKey}. MessageTopic: {messageTopic}. TopicPartitionOffset: {topicPartitionOffset}")]
  static partial void LogRedirectedKafkaMessage(ILogger logger, string? messageKey, string messageTopic, TopicPartitionOffset topicPartitionOffset, PersistenceStatus persistenceStatus);

  [LoggerMessage(93, LogLevel.Error, "Redirect kafka message error. MessageKey: {messageKey}.")]
  static partial void LogRedirectKafkaMessageError(ILogger logger, string? messageKey, Exception exception);

  [LoggerMessage(94, LogLevel.Error, "Redirect kafka message critical error. MessageKey: {messageKey}.")]
  static partial void LogRedirectKafkaMessageCriticalError(ILogger logger, string? messageKey, Exception exception);

  [LoggerMessage(95, LogLevel.Warning, "Redirect kafka message delivery warning. MessageKey: {messageKey}. Warning: {warning}.")]
  static partial void LogRedirectKafkaMessageDeliveryWarning(ILogger logger, string? messageKey, string? warning);

  static void InstrumentRedirectedKafkaMessage(
    string? messageKey,
    string messageTopic,
    TopicPartitionOffset topicPartitionOffset,
    PersistenceStatus persistenceStatus,
    IInstrumentationServices services)
  {
    LogRedirectedKafkaMessage(services.GetLogger(), messageKey, messageTopic, topicPartitionOffset, persistenceStatus);
    AddMetricCounter(RedirectedKafkaCounter);
    AddActivityTag(Activity.Current, "redirecting.kafka.key", messageKey);
    AddActivityTag(Activity.Current, "redirecting.kafka.topic", messageTopic);
    AddActivityEvent(Activity.Current, "redirecting.kafka");
  }

  static void InstrumentRedirectKafkaMessageError(
    string? messageKey,
    Exception ex,
    IInstrumentationServices services)
  {
    LogRedirectKafkaMessageError(services.GetLogger(), messageKey, ex);
    AddMetricCounter(RedirectKafkaErrorCounter);
    AddActivityEvent(Activity.Current, "redirect.kafka.error", [
      CreateActivityEventAttribute("redirect.error", ex),
    ]);
  }

  static void InstrumentRedirectKafkaMessageCriticalError(
    string? messageKey,
    Exception ex,
    IInstrumentationServices services)
  {
    LogRedirectKafkaMessageCriticalError(services.GetLogger(), messageKey, ex);
    AddMetricCounter(RedirectKafkaCriticalErrorCounter);
    AddActivityEvent(Activity.Current, "redirect.kafka.critical.error", [
      CreateActivityEventAttribute("redirect.error", ex),
    ]);
  }

  static void InstrumentRedirectKafkaMessageDeliveryWarning(
    string? messageKey,
    string? warning,
    IInstrumentationServices services)
  {
    LogRedirectKafkaMessageDeliveryWarning(services.GetLogger(), messageKey, warning);
    AddMetricCounter(RedirectKafkaDeliveryWarningCounter);
    AddActivityEvent(Activity.Current, "redirect.kafka.delivery.warning", [
      CreateActivityEventAttribute("redirect.warning", warning),
    ]);
  }
}