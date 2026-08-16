
namespace Kafka.Operations.DeadLetter;

 public interface IInsertingServices<TKey, TPayload> :
  IInsertDeadLetterMessageService<TKey, TPayload>,
  IInstrumentationServices;