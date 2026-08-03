
namespace Kafka.Engine;

partial class EngineFuncs
{
  static void InstrumentApplyConsumerOffsetStep(
    Activity activity,
    TopicPartitionOffset? offset,
    IInstrumentationServices services)
  {
    LogAppliedConsumerOffset(services.GetLogger());
    AddActivityTag(activity, "offset.applied", offset);
  }
}