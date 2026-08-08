
namespace Kafka.Operations.Inbox;

partial class InboxFuncs
{
  [LoggerMessage(7, LogLevel.Information, "Offset consumer. MessageId: {messageId}. Offset: {offset}")]
  static partial void LogOffsetConsumer(ILogger logger, Guid? messageId, TopicPartitionOffset? offset);

  [LoggerMessage(8, LogLevel.Information, "Offset consumer with missing message. Offset: {offset}")]
  static partial void LogOffsetConsumerMissingMessage(ILogger logger, TopicPartitionOffset? offset);

  [LoggerMessage(9, LogLevel.Error, "Offset consumer error. MessageId: {messageId}. TopicPartitionOffset: {offset}.")]
  static partial void LogOffsetConsumerError(ILogger logger, Guid? messageId, TopicPartitionOffset? offset, Exception ex);

  static void InstrumentOffsetConsumer(
    Guid? messageId,
    TopicPartitionOffset? offset,
    IInstrumentationServices services)
  {
    LogOffsetConsumer(services.GetLogger(), messageId, offset);
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
    Guid? messageId,
    TopicPartitionOffset? offset,
    IInstrumentationServices services)
  {
    LogOffsetConsumerError(services.GetLogger(), messageId, offset, ex);
    AddActivityTag(Activity.Current, "offset.consumer", offset);
    AddActivityTag(Activity.Current, "offset.consumer.error", ex);
    AddActivityEvent(Activity.Current, "offset.consumer.error");
  }
}