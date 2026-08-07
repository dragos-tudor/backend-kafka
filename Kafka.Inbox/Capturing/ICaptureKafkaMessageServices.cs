
namespace Kafka.Inbox;

public interface ICaptureKafkaMessageServices<TKey, TValue> :
  IInstrumentationServices,
  IConsumerService<TKey, TValue>;