
namespace Kafka.Engine;

public interface IRelayOutboxMessagesServices<TKey, TValue, TPaylod> :
  IGetLogger;