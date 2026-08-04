
namespace Kafka.Operations;

partial class OperationsFuncs
{
  [LoggerMessage(4, LogLevel.Debug, "Offset applied to consumer.")]
  static partial void LogOffsetConsumerApplied(ILogger logger);

  static void InstrumentOffsetConsumer(
    TopicPartitionOffset? offset,
    IInstrumentationServices services)
  {
    LogOffsetConsumerApplied(services.GetLogger());
    AddActivityTag(Activity.Current, "consumer,offset", offset);
  }
}