
namespace Kafka.Engine;

partial class EngineFuncs
{
  static void InstrumentInsertInboxMessageStep(
    Activity activity,
    IInstrumentationServices service)
  {
    LogInsertedInboxMessage(service.GetLogger());
    AddActivityEvent(activity, "inbox.message.inserted");
  }
}