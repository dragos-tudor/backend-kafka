
namespace Kafka.Engine;

partial class EngineFuncs
{
  static Activity? InstrumentHandleInboxMessageStep(
    Activity activity,
    IInstrumentationServices services)
  {
    IncrementMetricCounter(services.GetMetricCounters(), MetricCounterTypes.Handled);

    LogHandledInboxMessage(services.GetLogger());
    AddActivityEvent(activity, "message.handled");
    return activity;
  }

  private static Activity? InstrumentHandleInboxMessageErrorStep(
    Activity activity,
    string domainError,
    IInstrumentationServices services)
  {
    LogHandledInboxMessageFailed(services.GetLogger(), domainError);
    AddActivityTag(activity, "domain.error", domainError);
    AddActivityEvent(activity, "message.handling.failed",
      [CreateActivityEventAttribute("domain.error", domainError)]);
    return activity;
  }
}