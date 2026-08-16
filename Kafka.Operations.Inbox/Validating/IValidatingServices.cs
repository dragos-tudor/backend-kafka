
namespace Kafka.Operations.Inbox;

public interface IValidatingServices<TKey, TPayload> :
  IInboxPayloadValidator<TPayload>,
  IInstrumentationServices;