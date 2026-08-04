
namespace Kafka.StateMachines;

public interface IRelayOutboxMessagesServices<TKey, TValue, TPaylod> :
  ILoggerService;