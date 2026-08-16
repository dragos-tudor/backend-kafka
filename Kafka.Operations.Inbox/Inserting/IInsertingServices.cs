
namespace Kafka.Operations.Inbox;

 public interface IInsertingServices<TKey, TPayload> :
  IInsertInboxMessageService<TKey, TPayload>,
  IInstrumentationServices;