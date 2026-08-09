
namespace Kafka.Messages;

public enum OutboxMessageStatus { Pending, Published, Dispatching, Dispatched, Abandoned }