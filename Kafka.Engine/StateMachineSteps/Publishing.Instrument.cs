
namespace Kafka.Engine;

partial class EngineFuncs
{
  static void InstrumentPublishDeadLetterStep(
    Activity activity,
    string? domainError,
    string deadLetterTopic,
    IInstrumentationServices services)
  {
    LogPublishedDeadLetter(services.GetLogger());

    var metricCounters = services.GetMetricCounters();
    IncrementMetricCounter(metricCounters, MetricCounterTypes.DeadLettered);

    AddActivityTag(activity, "deadletter.topic", deadLetterTopic);
    AddActivityTag(activity, "deadletter.reason", domainError);
    AddActivityEvent(activity, "deadletter.published");
  }
}