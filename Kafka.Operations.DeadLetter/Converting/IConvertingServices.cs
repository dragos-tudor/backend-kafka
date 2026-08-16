
namespace Kafka.Operations.DeadLetter;

 public interface IConvertingServices<TKey, TValue, TPayload> :
  IInstrumentationServices,
  IUtcDateService;