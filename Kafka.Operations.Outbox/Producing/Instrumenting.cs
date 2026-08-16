using static Kafka.Operations.Outbox.ProducingCounters;

namespace Kafka.Operations.Outbox;

partial class OutboxFuncs
{
  [LoggerMessage(92, LogLevel.Information, "Producing kafka message. MessageKey: {messageKey}. MessageTopic: {messageTopic}. Handle error: {handleError}")]
  static partial void LogProducingKafkaMessage(ILogger logger, string? messageKey, string messageTopic, string? handleError);

  [LoggerMessage(93, LogLevel.Error, "Produce kafka message error. MessageKey: {messageKey}.")]
  static partial void LogProduceKafkaMessageError(ILogger logger, string? messageKey, Exception exception);

  [LoggerMessage(94, LogLevel.Error, "Produce kafka message critical error. MessageKey: {messageKey}.")]
  static partial void LogProduceKafkaMessageCriticalError(ILogger logger, string? messageKey, Exception exception);

  [LoggerMessage(91, LogLevel.Information, "Produced kafka message. MessageKey: {messageKey}. TopicPartitionOffset: {topicPartitionOffset}.")]
  static partial void LogProducedCallback(ILogger logger, string? messageKey, TopicPartitionOffset topicPartitionOffset);

  [LoggerMessage(95, LogLevel.Error, "Produce kafka message callback delivery error. MessageKey: {messageKey}. Error: {error}.")]
  static partial void LogProduceCallbackDeliveryError(ILogger logger, string? messageKey, string? error);

  [LoggerMessage(96, LogLevel.Error, "Produce kafka message callback error. MessageKey: {messageKey}.")]
  static partial void LogProduceCallbackError(ILogger logger, string? messageKey, Exception exception);

  static void InstrumentProducingKafkaMessage(
    string? messageKey,
    string messageTopic,
    IInstrumentationServices services)
  {
    LogProducingKafkaMessage(services.GetLogger(), messageKey, messageTopic, null);
    AddMetricCounter(ProducingKafkaCounter);
    AddActivityTag(Activity.Current, "producing.kafka.key", messageKey);
    AddActivityTag(Activity.Current, "producing.kafka.topic", messageTopic);
    AddActivityEvent(Activity.Current, "producing.kafka");
  }

  static void InstrumentProduceKafkaMessageError(
    string? messageKey,
    Exception ex,
    IInstrumentationServices services)
  {
    LogProduceKafkaMessageError(services.GetLogger(), messageKey, ex);
    AddMetricCounter(ProduceKafkaErrorCounter);
    AddActivityEvent(Activity.Current, "produce.kafka.error", [
      CreateActivityEventAttribute("produce.error", ex),
    ]);
  }

  static void InstrumentProduceKafkaMessageCriticalError(
    string? messageKey,
    Exception ex,
    IInstrumentationServices services)
  {
    LogProduceKafkaMessageCriticalError(services.GetLogger(), messageKey, ex);
    AddMetricCounter(ProduceKafkaCriticalErrorCounter);
    AddActivityEvent(Activity.Current, "produce.kafka.critical.error", [
      CreateActivityEventAttribute("produce.error", ex),
    ]);
  }

  static void InstrumentProducedCallback(
    string? messageKey,
    TopicPartitionOffset topicPartitionOffset,
    IInstrumentationServices services)
  {
    LogProducedCallback(services.GetLogger(), messageKey, topicPartitionOffset);
    AddMetricCounter(ProducingKafkaCounter);
    AddActivityTag(Activity.Current, "produced.callback.key", messageKey);
    AddActivityTag(Activity.Current, "produced.callback.topic", topicPartitionOffset.Topic);
    AddActivityEvent(Activity.Current, "produced.callback");
  }

  static void InstrumentProduceCallbackError(
    string? messageKey,
    Exception ex,
    IInstrumentationServices services)
  {
    LogProduceCallbackError(services.GetLogger(), messageKey, ex);
    AddMetricCounter(ProduceKafkaErrorCounter);
    AddActivityEvent(Activity.Current, "produce.callback.error", [
      CreateActivityEventAttribute("produce.error", ex),
    ]);
  }

  static void InstrumentProduceCallbackDeliveryError(
    string? messageKey,
    string? error,
    IInstrumentationServices services)
  {
    LogProduceCallbackDeliveryError(services.GetLogger(), messageKey, error);
    AddMetricCounter(ProduceKafkaDeliveryErrorCounter);
    AddActivityEvent(Activity.Current, "produce.callback.delivery.error", [
      CreateActivityEventAttribute("produce.error", error),
    ]);
  }
}