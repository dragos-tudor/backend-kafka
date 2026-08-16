using static Kafka.Operations.DeadLetter.ProducingCounters;

namespace Kafka.Operations.DeadLetter;

partial class DeadLetterFuncs
{
  [LoggerMessage(92, LogLevel.Information, "Producing kafka dead letter. MessageKey: {messageKey}. MessageTopic: {messageTopic}. Handle error: {handleError}")]
  static partial void LogProducingKafkaDeadLetter(ILogger logger, string? messageKey, string messageTopic, string? handleError);

  [LoggerMessage(93, LogLevel.Error, "Produce kafka dead letter error. MessageKey: {messageKey}.")]
  static partial void LogProduceKafkaDeadLetterError(ILogger logger, string? messageKey, Exception exception);

  [LoggerMessage(94, LogLevel.Error, "Produce kafka dead letter critical error. MessageKey: {messageKey}.")]
  static partial void LogProduceKafkaDeadLetterCriticalError(ILogger logger, string? messageKey, Exception exception);

  [LoggerMessage(91, LogLevel.Information, "Produced kafka dead letter callback. MessageKey: {messageKey}. TopicPartitionOffset: {topicPartitionOffset}.")]
  static partial void LogProducedDeadLetterCallback(ILogger logger, string? messageKey, TopicPartitionOffset topicPartitionOffset);

  [LoggerMessage(95, LogLevel.Error, "Produce kafka dead letter callback delivery error. MessageKey: {messageKey}. Error: {error}.")]
  static partial void LogProduceDeadLetterCallbackDeliveryError(ILogger logger, string? messageKey, string? error);

  [LoggerMessage(96, LogLevel.Error, "Produce kafka dead letter callback error. MessageKey: {messageKey}.")]
  static partial void LogProduceDeadLetterCallbackError(ILogger logger, string? messageKey, Exception exception);

  static void InstrumentProducingKafkaDeadLetter(
    string? messageKey,
    string messageTopic,
    IInstrumentationServices services)
  {
    LogProducingKafkaDeadLetter(services.GetLogger(), messageKey, messageTopic, null);
    AddMetricCounter(ProducingKafkaCounter);
    AddActivityTag(Activity.Current, "producing.kafka.deadletter.key", messageKey);
    AddActivityTag(Activity.Current, "producing.kafka.deadletter.topic", messageTopic);
    AddActivityEvent(Activity.Current, "producing.kafka.deadletter");
  }

  static void InstrumentProduceKafkaDeadLetterError(
    string? messageKey,
    Exception ex,
    IInstrumentationServices services)
  {
    LogProduceKafkaDeadLetterError(services.GetLogger(), messageKey, ex);
    AddMetricCounter(ProduceKafkaErrorCounter);
    AddActivityEvent(Activity.Current, "produce.kafka.deadletter.error", [
      CreateActivityEventAttribute("produce.error", ex),
    ]);
  }

  static void InstrumentProduceKafkaDeadLetterCriticalError(
    string? messageKey,
    Exception ex,
    IInstrumentationServices services)
  {
    LogProduceKafkaDeadLetterCriticalError(services.GetLogger(), messageKey, ex);
    AddMetricCounter(ProduceKafkaCriticalErrorCounter);
    AddActivityEvent(Activity.Current, "produce.kafka.deadletter.critical.error", [
      CreateActivityEventAttribute("produce.error", ex),
    ]);
  }

  static void InstrumentProducedDeadLetterCallback(
    string? messageKey,
    TopicPartitionOffset topicPartitionOffset,
    IInstrumentationServices services)
  {
    LogProducedDeadLetterCallback(services.GetLogger(), messageKey, topicPartitionOffset);
    AddMetricCounter(ProducingKafkaCounter);
    AddActivityTag(Activity.Current, "produced.kafka.deadletter.callback.key", messageKey);
    AddActivityTag(Activity.Current, "produced.kafka.deadletter.callback.topic", topicPartitionOffset.Topic);
    AddActivityEvent(Activity.Current, "produced.kafka.deadletter.callback");
  }

  static void InstrumentProduceDeadLetterCallbackError(
    string? messageKey,
    Exception ex,
    IInstrumentationServices services)
  {
    LogProduceDeadLetterCallbackError(services.GetLogger(), messageKey, ex);
    AddMetricCounter(ProduceKafkaErrorCounter);
    AddActivityEvent(Activity.Current, "produce.kafka.deadletter.callback.error", [
      CreateActivityEventAttribute("produce.error", ex),
    ]);
  }

  static void InstrumentProduceDeadLetterCallbackDeliveryError(
    string? messageKey,
    string? error,
    IInstrumentationServices services)
  {
    LogProduceDeadLetterCallbackDeliveryError(services.GetLogger(), messageKey, error);
    AddMetricCounter(ProduceKafkaDeliveryErrorCounter);
    AddActivityEvent(Activity.Current, "produce.kafka.deadletter.callback.delivery.error", [
      CreateActivityEventAttribute("produce.error", error),
    ]);
  }
}