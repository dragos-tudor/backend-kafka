
namespace Kafka.Operations.Inbox;

public interface ICaptureKafkaMessageServices<TKey, TValue> :
  IInstrumentationServices,
  IConsumerService<TKey, TValue>;