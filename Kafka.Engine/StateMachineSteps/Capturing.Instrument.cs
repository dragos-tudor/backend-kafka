
namespace Kafka.Engine;

partial class EngineFuncs
{
  static void InstrumentCaptureKafkaMessageStep(
    Activity activity,
    IInstrumentationServices services)
  {
    LogCapturedKafkaMessage(services.GetLogger());
    IncrementMetricCounter(services.GetMetricCounters(), MetricCounterTypes.Captured);
    AddActivityEvent(activity, "message.captured");
  }
}