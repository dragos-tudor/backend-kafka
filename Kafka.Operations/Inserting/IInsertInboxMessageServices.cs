
namespace Kafka.Operations;

 public interface IInsertInboxMessageServices<TKey, TValue, TPayload> :
  IInboxMessageMapperService<TValue, TPayload>,
  IInsertInboxMessageService<TKey, TPayload>,
  IInstrumentationServices,
  IUtcDateService;