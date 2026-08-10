
namespace Kafka.Operations.Inbox;

partial class InboxFuncs
{
  [LoggerMessage(7, LogLevel.Information, "Offset consumer. Offset: {offset}")]
  static partial void LogOffsetConsumer(ILogger logger, TopicPartitionOffset? offset);

  [LoggerMessage(8, LogLevel.Information, "Offset consumer with missing message. Offset: {offset}")]
  static partial void LogOffsetConsumerMissingMessage(ILogger logger, TopicPartitionOffset? offset);

  [LoggerMessage(9, LogLevel.Error, "Offset consumer error. TopicPartitionOffset: {offset}.")]
  static partial void LogOffsetConsumerError(ILogger logger, TopicPartitionOffset? offset, Exception ex);

  static void InstrumentOffsetConsumer(
    TopicPartitionOffset? offset,
    IInstrumentationServices services)
  {
    LogOffsetConsumer(services.GetLogger(), offset);
    AddActivityTag(Activity.Current, "offset.consumer", offset);
    AddActivityEvent(Activity.Current, "offset.consumer");
  }

  static void InstrumentOffsetConsumerMissingMessage(
    TopicPartitionOffset? offset,
    IInstrumentationServices services)
  {
    LogOffsetConsumerMissingMessage(services.GetLogger(), offset);
    AddActivityTag(Activity.Current, "offset.consumer", offset);
    AddActivityEvent(Activity.Current, "offset.consumer.missing.message");
  }

  static void InstrumentOffsetConsumerError(
    Exception ex,
    TopicPartitionOffset? offset,
    IInstrumentationServices services)
  {
    LogOffsetConsumerError(services.GetLogger(), offset, ex);
    AddActivityTag(Activity.Current, "offset.consumer", offset);
    AddActivityTag(Activity.Current, "offset.consumer.error", ex);
    AddActivityEvent(Activity.Current, "offset.consumer.error");
  }
}