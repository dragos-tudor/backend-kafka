
namespace Kafka.Operations.Inbox;

public interface ICapturingServices<TKey, TValue> :
  IInstrumentationServices,
  IConsumerService<TKey, TValue>;