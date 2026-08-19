
namespace Kafka.Operations.DeadLetter;

 public interface IInsertingServices<TKey, TPayload> :
  IInsertDeadLetterMessageService<TKey, TPayload>,
  IUpdateInboxMessageService<TKey, TPayload>,
  IInstrumentationServices;