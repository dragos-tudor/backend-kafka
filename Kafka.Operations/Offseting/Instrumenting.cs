
namespace Kafka.Operations;

partial class OperationsFuncs
{
  [LoggerMessage(4, LogLevel.Debug, "Offset applied to consumer. MessageId: {messageId}. Offset: {offset}")]
  static partial void LogOffsetConsumerApplied(ILogger logger, Guid? messageId, TopicPartitionOffset? offset);

  static void InstrumentOffsetConsumerFailed(
    Guid? messageId,
    TopicPartitionOffset? offset,
    IInstrumentationServices services)
  {
    LogOffsetConsumerApplied(services.GetLogger(), messageId, offset);
    AddActivityTag(Activity.Current, "consumer.offset.failed", offset);
  }

  static void InstrumentOffsetConsumer(
    Guid? messageId,
    TopicPartitionOffset? offset,
    IInstrumentationServices services)
  {
    LogOffsetConsumerApplied(services.GetLogger(), messageId, offset);
    AddActivityTag(Activity.Current, "consumer.offset", offset);
  }
}