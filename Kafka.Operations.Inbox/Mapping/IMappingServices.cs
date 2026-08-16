
namespace Kafka.Operations.Inbox;

 public interface IMappingServices<TKey, TValue, TPayload> :
  IInboxPayloadMapper<TValue, TPayload>,
  IInstrumentationServices,
  IUtcDateService;